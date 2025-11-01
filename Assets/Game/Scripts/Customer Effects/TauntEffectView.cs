using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class TauntEffectView : CustomerEffectView<TauntEffectInstance>
    {
        [SerializeField] private GameObject tauntIsActiveIcon;
        [SerializeField] private GameObject tauntIsUnactiveIcon;
        [SerializeField] private Image tauntIsUnactiveProgressIcon;
        [SerializeField] private AudioSource tauntActivateSound;
        [SerializeField] private AudioSource tauntDeactivateSound;

        protected override void OnInit()
        {
            Instance.OnStateChanged += StateChangedHandler;
            Instance.OnTick += TickHandler;
            StateChangedHandler(true);
        }

        private async void StateChangedHandler(bool state)
        {
            tauntIsActiveIcon.SetActive(state);
            tauntIsUnactiveIcon.SetActive(!state);
            tauntIsUnactiveProgressIcon.gameObject.SetActive(!state);

            if (state)
            {
                tauntActivateSound.Play();
                PlayAnimation(tauntIsActiveIcon.transform);
            }
            else
            {
                tauntDeactivateSound.Play();
                PlayAnimation(tauntIsUnactiveIcon.transform);
            }
        }

        private async void PlayAnimation(Transform icon, float animationTime = 0.3f)
        {
            icon.transform.DORotate(new Vector3(0, 0, -30), animationTime * 0.5f);
            await icon.DOScale(3f, animationTime * 0.5f).AsyncWaitForCompletion();
            icon.DORotate(new Vector3(0, 0, 0), animationTime * 0.5f);
            icon.DOScale(1f, animationTime * 0.5f);
        }

        private void TickHandler(float timer, float cooldown)
        {
            tauntIsUnactiveProgressIcon.fillAmount = timer / cooldown;
        }
    }
}