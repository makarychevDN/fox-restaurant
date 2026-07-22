using Cysharp.Threading.Tasks;
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
        [SerializeField] private Animator animator;

        protected override void OnInit(RestaurantEncounter restaurantEncounter)
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
                tauntActivateSound.Play();
            else
                tauntDeactivateSound.Play();

            animator.SetTrigger("triggered");
        }

        private void TickHandler(float timer, float cooldown)
        {
            tauntIsUnactiveProgressIcon.fillAmount = timer / cooldown;
        }
    }
}