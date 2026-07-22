using UnityEngine;
using UnityEngine.UI;


namespace foxRestaurant
{
    public class ColdEffectView : CustomerEffectView<ColdEffectInstance>
    {
        [SerializeField] private Image sneezeProgressionIcon;
        [SerializeField] private Transform iconsParent;
        [SerializeField] private AudioSource sneezeSound;
        [SerializeField] private AudioSource coldCuredSound;
        [SerializeField] private Animator animator;

        protected override void OnInit(RestaurantEncounter restaurantEncounter)
        {
            Instance.OnTick += TickHandler;
            Instance.OnSneeze += OnSneezeHandler;
            Instance.OnColdCured += OnColdCuredHandler;
        }

        private void TickHandler(float timer, float cooldown)
        {
            sneezeProgressionIcon.fillAmount = timer / cooldown;
        }

        private void OnSneezeHandler()
        {
            sneezeSound.Play();
            Camera.main.ShakeCamera(1);
            animator.SetTrigger("triggered");
        }

        private void OnColdCuredHandler()
        {
            coldCuredSound.Play();
            animator.SetTrigger("triggered");
        }
    }
}