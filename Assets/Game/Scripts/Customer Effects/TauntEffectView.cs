using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class TauntEffectView : CustomerEffectView<TauntEffectInstance>
    {
        [SerializeField] private GameObject tauntIsActiveIcon;
        [SerializeField] private GameObject tauntIsUnactiveIcon;
        [SerializeField] private Image tauntIsUnactiveProgressIcon;

        protected override void OnInit()
        {
            Instance.OnStateChanged += StateChangedHandler;
            Instance.OnTick += TickHandler;
        }

        private void StateChangedHandler(bool state)
        {
            tauntIsActiveIcon.SetActive(state);
            tauntIsUnactiveIcon.SetActive(!state);
            tauntIsUnactiveProgressIcon.gameObject.SetActive(!state);
        }

        private void TickHandler(float timer, float cooldown)
        {
            tauntIsUnactiveProgressIcon.fillAmount = timer / cooldown;
        }
    }
}