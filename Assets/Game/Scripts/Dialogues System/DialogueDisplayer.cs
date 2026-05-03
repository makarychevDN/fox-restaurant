using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class DialogueDisplayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text actualText;
        [SerializeField] private GameObject panel;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private VerticalLayoutGroup rootVerticalLayoutGroup;
        [SerializeField] private List<RectTransform> contentSizeFitters;
        [SerializeField] private float pitchDelta = 0;
        [SerializeField] private float pitch = 1;

        private Vector3 hashedLocalPosition;
        private TextAnchor hashedRootChildAlignment;
        private Centering hashedCentering;

        public void SetSound(AudioClip sound) => audioSource.clip = sound;
        public void SetPitch(string inputValue) => pitch = inputValue.ParseFloatSafe();
        public void SetPitchDelta(string inputValue) => pitchDelta = inputValue.ParseFloatSafe();
        public void SetVolume(string inputValue) => audioSource.volume = inputValue.ParseFloatSafe();

        private void Awake()
        {
            hashedCentering = Centering.Left;
            hashedLocalPosition = transform.localPosition;
            hashedRootChildAlignment = rootVerticalLayoutGroup.childAlignment;
        }

        public void Print(int substringLength, bool playSound = true)
        {
            actualText.maxVisibleCharacters += substringLength;

            if (playSound)
            {
                audioSource.pitch = Random.Range(pitch - pitchDelta, pitch + pitchDelta);
                audioSource.Play();
            }
        }

        public void Show(string wholeString)
        {
            panel.SetActive(true);
            actualText.text = PreprocessText(wholeString);
            actualText.maxVisibleCharacters = 0;
            RebuildContentSizeFitters();
        }

        public void Hide()
        {
            panel.SetActive(false);
        }

        private void RebuildContentSizeFitters()
        {
            foreach(var fitter in contentSizeFitters)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(fitter);
            }
        }

        public string PreprocessText(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string processed = Regex.Replace(input, @"<color:([^>]+)>", "<color=$1>");
            processed = Regex.Replace(processed, @"<(pause|delay|voice|chunk|emote|pitch|volume|pitch delta)(:[^>]*)?>", string.Empty);

            return processed;
        }

        public void SetCentering(TextAnchor childAlignment)
        {
            rootVerticalLayoutGroup.childAlignment = childAlignment;
            hashedRootChildAlignment = childAlignment;
        }

        public void SetCentering(Centering centering)
        {
            switch (centering)
            {
                case Centering.Left:
                    rootVerticalLayoutGroup.childAlignment = TextAnchor.MiddleLeft; break;
                case Centering.Center:
                    rootVerticalLayoutGroup.childAlignment = TextAnchor.MiddleCenter; break;
                case Centering.Right:
                    rootVerticalLayoutGroup.childAlignment = TextAnchor.MiddleRight; break;
            }

            sbyte delta = hashedCentering - centering;
            transform.localPosition += new Vector3((transform as RectTransform).sizeDelta.x * delta * 0.5f, 0);
            hashedCentering = centering;
        }

        public enum Centering : sbyte
        {
            Left = -1,
            Center = 0,
            Right = 1
        }
    }
}