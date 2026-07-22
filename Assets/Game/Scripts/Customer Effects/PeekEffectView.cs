using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace foxRestaurant
{
    public class PeekEffectView : CustomerEffectView<PeekEffectInstance>
    {
        [SerializeField] private Transform icon;
        [SerializeField] private AudioSource effectTriggeredSound;
        [SerializeField] private Animator animator;

        protected override void OnInit(RestaurantEncounter restaurantEncounter)
        {
            Instance.OnTriggered += PlayAnimation;
        }

        private async void PlayAnimation()
        {
            effectTriggeredSound.Play();
            animator.SetTrigger("triggered");
        }
    }
}