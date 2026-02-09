using DG.Tweening;
using UnityEngine;

namespace foxRestaurant
{
    public class PeekEffectView : CustomerEffectView<PeekEffectInstance>
    {
        [SerializeField] private Transform icon;
        [SerializeField] private AudioSource effectTriggeredSound;

        protected override void OnInit()
        {
            Instance.OnTriggered += PlayAnimation;
        }

        private async void PlayAnimation()
        {
            float animationTime = 0.3f;
            effectTriggeredSound.Play();
            icon.transform.DORotate(new Vector3(0, 0, -30), animationTime * 0.5f);
            await icon.DOScale(3f, animationTime * 0.5f).AsyncWaitForCompletion();
            icon.DORotate(new Vector3(0, 0, 0), animationTime * 0.5f);
            icon.DOScale(1f, animationTime * 0.5f);
        }
    }
}