using UnityEngine;

namespace foxRestaurant
{
    public class FeverEffectView : CustomerEffectView<FeverEffectInstance>
    {
        [SerializeField] private Transform icon;
        [SerializeField] private GameObject activeState;
        [SerializeField] private GameObject curedState;
        [SerializeField] private AudioSource effectSound;
        [SerializeField] private Animator animator;
        private RestaurantEncounter restaurantEncounter;

        protected override void OnInit(RestaurantEncounter restaurantEncounter)
        {
            Instance.OnTriggeredOnCertainCustomer += OnTriggeredOnCertainCustomerHandler;
            Instance.OnEffectIsCured += OnEffectCuredHandler;
            this.restaurantEncounter = restaurantEncounter;
        }

        private void OnTriggeredOnCertainCustomerHandler(Customer customer)
        {
            animator.SetTrigger("triggered");
        }

        private void OnEffectCuredHandler()
        {
            animator.SetTrigger("triggered");
            activeState.SetActive(false);
            curedState.SetActive(true);
        }
    }
}