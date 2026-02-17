using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;

namespace foxRestaurant
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<LookForCursorController> eyesAndNose;

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public async Task Say(string text)
        {
            await dialogueManager.DisplayDialogueLine(text);
        }

        public async Task Say(LocalizedString text)
        {
            await dialogueManager.DisplayDialogueLine(text.GetLocalizedString());
        }

        public void LookAt(Transform target)
        {
            eyesAndNose.ForEach(item => item.SetTarget(target));
        }

        public UnityEvent<string> OnEmote => dialogueManager.OnEmote;
    }
}