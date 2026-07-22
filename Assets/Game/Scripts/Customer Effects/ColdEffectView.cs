using Cysharp.Threading.Tasks;
using DG.Tweening;
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

        private async void PlayAnimation(Transform icon, float animationTime = 0.3f)
        {
            icon.transform.DORotate(new Vector3(0, 0, -30), animationTime * 0.5f);
            await icon.DOScale(3f, animationTime * 0.5f).ToUniTask();
            icon.DORotate(new Vector3(0, 0, 0), animationTime * 0.5f);
            icon.DOScale(1f, animationTime * 0.5f);
        }

        private void OnSneezeHandler()
        {
            sneezeSound.Play();
            Camera.main.ShakeCamera(1);
            PlayAnimation(iconsParent);
        }

        private void OnColdCuredHandler()
        {
            coldCuredSound.Play();
            PlayAnimation(iconsParent);
        }
    }
}