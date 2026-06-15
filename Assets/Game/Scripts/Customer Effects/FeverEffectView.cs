using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace foxRestaurant
{
    public class FeverEffectView : CustomerEffectView<FeverEffectInstance>
    {
        [SerializeField] private Transform icon;
        [SerializeField] private AudioSource effectSound;

        protected override void OnInit()
        {
            Instance.OnTriggeredOnCertainCustomer += OnTriggeredOnCertainCustomerHandler;
        }

        private void OnTriggeredOnCertainCustomerHandler(Customer customer)
        {
            PlayAnimation(icon);
            effectSound.Play();
        }

        private async void PlayAnimation(Transform icon, float animationTime = 0.3f)
        {
            icon.transform.DORotate(new Vector3(0, 0, -30), animationTime * 0.5f);
            await icon.DOScale(3f, animationTime * 0.5f).ToUniTask();
            icon.DORotate(new Vector3(0, 0, 0), animationTime * 0.5f);
            icon.DOScale(1f, animationTime * 0.5f);
        }
    }
}