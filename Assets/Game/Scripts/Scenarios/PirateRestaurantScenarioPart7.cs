using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;

namespace foxRestaurant
{
    public class PirateRestaurantScenarioPart7 : BaseScenario<ListenDialoguesEncounter>
    {
        [Header("turn tape scene")]
        [SerializeField] private GameObject parentTurnTapeObject;
        [SerializeField] private Transform paw;
        [SerializeField] private Transform tape;
        [SerializeField] private AudioSource squekSounds;
        [SerializeField] private ParticleSystem water;
        [SerializeField] private ParticleSystem steam;
        [SerializeField] private AudioSource newMessageSound;
        [SerializeField] private Character redInBathroomIntro;

        [Header("bathroom mobile phone scene")]
        [SerializeField] private GameObject bathroomPhoneScene;
        [SerializeField] private Transform phoneAndPawsParentInBathroom;
        [SerializeField] private Transform rightPaw;
        [SerializeField] private GameObject blockedScreen;
        [SerializeField] private Character redInBathroomWithPhone;
        [SerializeField] private AudioSource unlockPhoneSound;

        [Header("kitchen intro scene")]
        [SerializeField] private GameObject parentIntroKitchenScene;
        [SerializeField] private AudioSource switchSound;
        [SerializeField] private GameObject lightenKitchenBackground;
        [SerializeField] private GameObject darkKitchenBackground;
        [SerializeField] private Character silverOnIntroKitchen;
        [SerializeField] private Character redOnIntroKitchen;

        [Header("main kitchen scene")]
        [SerializeField] private GameObject parentMainKitchenScene;
        [SerializeField] private Character silverOnMainKitchen;
        [SerializeField] private Character redOnMainKitchen;
        [SerializeField] private AudioSource lookingForJarSounds;
        [SerializeField] private AudioSource typingSounds;
        [SerializeField] private AudioSource sadFoxSound;
        [SerializeField] private GameObject redsPawsEmpty;
        [SerializeField] private GameObject redsPawsWithJar;
        [SerializeField] private GameObject redsPawsWithPhone;
        [SerializeField] private GameObject jarOnTheTable;
        [SerializeField] private GameObject packOfMoney;
        [SerializeField] private Transform silversEyes;

        [Header("main kitchen scene (red's sprites setup)")]
        [SerializeField] SpriteRenderer body;
        [SerializeField] SpriteRenderer leftEye, rightEye;
        [SerializeField] SpriteMask leftEyeMask, rightEyeMask;
        [SerializeField] Sprite calmBody, calmLeftEye, calmRightEye;
        [SerializeField] Sprite sadBody, sadLeftEye, sadRightEye;

        [Header("show jar scene")]
        [SerializeField] private AudioSource music;
        [SerializeField] private GameObject showJarScene;
        [SerializeField] private Transform pawsAndJar;
        [SerializeField] private Character redOnShowingJarScene;

        [Header("handshake scene")]
        [SerializeField] private GameObject handshakeScene;
        [SerializeField] private Character silverHandshake;
        [SerializeField] private Character redHandshake;
        [SerializeField] private Character storyTeller;
        [SerializeField] private Transform hadshakeParent;
        [SerializeField] private Transform redsHandshakePaw;
        [SerializeField] private Transform silversHandshakePaw;
        [SerializeField] private AudioSource handshakeImpactSound;
        [SerializeField] private GameObject theGreatestAdventureBeginsLogo;

        [SerializeField] List<LocalizedString> dialogueLines;
        private int stringsCounter = 0;
        private int Next => stringsCounter++;
        private LocalizedString NextLine => dialogueLines[Next];


        protected override void InitTyped(ListenDialoguesEncounter encounter){}

        protected override async Task StartScenarioTyped(ListenDialoguesEncounter encounter)
        {
            await SceneInShower();
            await SceneWithMobilePhone();
            await IntroSceneInKitchen();
            await MainSceneInKitchen1();
            await ShowJar();
            await MainSceneInKitchen2();
            await HandshakeScene();
        }

        private async Task SceneInShower()
        {
            parentTurnTapeObject.gameObject.SetActive(true);
            var waterMain = water.main;
            var steamMain = steam.main;

            await paw.DOMove(new Vector3(0, -8), 0.5f).AsyncWaitForCompletion();
            squekSounds.Play();
            tape.DORotate(new Vector3(0, 0, 75), 0.5f);
            await paw.DOMove(new Vector3(0, -12.5f), 0.5f).AsyncWaitForCompletion();
            waterMain.maxParticles = 200;
            steamMain.maxParticles = 35;

            await paw.DOMove(new Vector3(0, -8), 0.5f).AsyncWaitForCompletion();
            tape.DORotate(new Vector3(0, 0, 75 * 2), 0.5f);
            await paw.DOMove(new Vector3(0, -12.5f), 0.5f).AsyncWaitForCompletion();
            waterMain.maxParticles = 100;
            steamMain.maxParticles = 20;

            await paw.DOMove(new Vector3(0, -8), 0.5f).AsyncWaitForCompletion();
            tape.DORotate(new Vector3(0, 0, 75 * 3), 0.5f);
            await paw.DOMove(new Vector3(0, -12.5f), 0.5f).AsyncWaitForCompletion();
            waterMain.maxParticles = 0;
            steamMain.maxParticles = 0;

            await Task.Delay(1500);
            await paw.DOMove(new Vector3(0, -30), 1f).AsyncWaitForCompletion();

            newMessageSound.Play();
            await Task.Delay(1500);
            await redInBathroomIntro.Say(NextLine);

            parentTurnTapeObject.gameObject.SetActive(false);
        }

        private async Task SceneWithMobilePhone()
        {
            bathroomPhoneScene.gameObject.SetActive(true);
            await phoneAndPawsParentInBathroom.DOMove(new Vector3(0, -18.8f), 0.75f).AsyncWaitForCompletion();
            await rightPaw.DOLocalMove(new Vector3(9f, 7f, 0), 0.5f).AsyncWaitForCompletion();
            blockedScreen.SetActive(false);
            unlockPhoneSound.Play();
            await rightPaw.DOLocalMove(new Vector3(11.85f, 0f), 0.5f).AsyncWaitForCompletion();
            await Task.Delay(500);
            await redInBathroomWithPhone.Say(NextLine);
            await phoneAndPawsParentInBathroom.DOMove(new Vector3(0, -43f), 0.75f).AsyncWaitForCompletion();
            bathroomPhoneScene.gameObject.SetActive(false);
        }

        private async Task IntroSceneInKitchen()
        {
            parentIntroKitchenScene.SetActive(true);
            darkKitchenBackground.SetActive(true);
            lightenKitchenBackground.SetActive(false);
            await DotweenSteps(redOnIntroKitchen.transform, new Vector3(-8, -8), new Vector3(1.1f, 0.75f), 0.8f, 3);
            await Task.Delay(1000);
            switchSound.Play();
            darkKitchenBackground.SetActive(false);
            lightenKitchenBackground.SetActive(true);
            silverOnIntroKitchen.LookAt(redOnIntroKitchen.transform);
            await Task.Delay(1000);
            await silverOnIntroKitchen.Say(NextLine);
            await redOnIntroKitchen.Say(NextLine);
            await silverOnIntroKitchen.Say(NextLine);
            await redOnIntroKitchen.Say(NextLine);
            await silverOnIntroKitchen.Say(NextLine);
            await redOnIntroKitchen.Say(NextLine);
            await silverOnIntroKitchen.Say(NextLine);
            parentIntroKitchenScene.SetActive(false);
        }

        private async Task MainSceneInKitchen1()
        {
            parentMainKitchenScene.SetActive(true);
            await DotweenSteps(redOnMainKitchen.transform, new Vector3(5.64f, -7.14f), new Vector3(1.25f, 0.75f), 1f, 3);
            await redOnMainKitchen.Say(NextLine);
            //как-то наколхозить объ€ти€
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            redOnMainKitchen.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            await DotweenSteps(redOnMainKitchen.transform, new Vector3(26f, -7.14f), new Vector3(1.25f, 0.75f), 1f, 3);
            redsPawsEmpty.SetActive(false);
            redsPawsWithJar.SetActive(true);
            lookingForJarSounds.Play();
            await Task.Delay(2500);
            redOnMainKitchen.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            await DotweenSteps(redOnMainKitchen.transform, new Vector3(5.64f, -7.14f), new Vector3(1.25f, 0.75f), 1f, 3);
            await redOnMainKitchen.Say(NextLine);
            parentMainKitchenScene.SetActive(false);
        }

        private async Task ShowJar()
        {
            showJarScene.SetActive(true);
            music.Play();
            await pawsAndJar.DOMove(new Vector3(0, -2f), 0.75f).AsyncWaitForCompletion();
            await pawsAndJar.DOMove(new Vector3(0, -3.5f), 0.15f).AsyncWaitForCompletion();
            await Task.Delay(1000);
            await redOnShowingJarScene.Say(NextLine);
            await redOnShowingJarScene.Say(NextLine);
            await redOnShowingJarScene.Say(NextLine);
            await redOnShowingJarScene.Say(NextLine);
            await redOnShowingJarScene.Say(NextLine);
            await redOnShowingJarScene.Say(NextLine);
            await redOnShowingJarScene.Say(NextLine);
            await redOnShowingJarScene.Say(NextLine);
            await pawsAndJar.DOMove(new Vector3(0, -30f), 0.35f).AsyncWaitForCompletion();
            showJarScene.SetActive(false);
        }

        private async Task MainSceneInKitchen2()
        {
            redsPawsEmpty.SetActive(true);
            redsPawsWithJar.SetActive(false);
            jarOnTheTable.SetActive(true);
            parentMainKitchenScene.SetActive(true);
            music.DOFade(0.25f, 1.5f);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await redsPawsEmpty.transform.DOScale(new Vector3(1, 0, 1), 0.5f).AsyncWaitForCompletion();
            redsPawsWithPhone.gameObject.SetActive(true);
            redOnMainKitchen.LookAt(redsPawsWithPhone.transform);
            await redsPawsWithPhone.transform.DOLocalMove(new Vector3(0, -1.35f, 0), 0.5f).AsyncWaitForCompletion();

            for(int i = 0; i < 2; i++)
            {
                typingSounds.Play();
                for (int j = 0; j < 20; j++)
                {
                    await redsPawsWithPhone.transform.DOShakeRotation(0.1f, 10).AsyncWaitForCompletion();
                }
                typingSounds.Pause();
                await Task.Delay(1000);
            }
            sadFoxSound.Play();
            ChangeRedsSprites(sadBody, sadLeftEye, sadRightEye);
            redOnMainKitchen.transform.DOShakeScale(0.1f, -0.1f);
            redOnMainKitchen.transform.DOMove(new Vector3(5.64f, -7.64f), 0.3f);

            await Task.Delay(2000);

            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            redOnMainKitchen.LookAt(silversEyes);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            packOfMoney.SetActive(true);
            redOnMainKitchen.LookAt(packOfMoney.transform);
            await Task.Delay(1500);
            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);

            ChangeRedsSprites(calmBody, sadLeftEye, sadRightEye);
            redOnMainKitchen.transform.DOShakeScale(0.1f, -0.1f);
            redOnMainKitchen.transform.DOMove(new Vector3(5.64f, -7.14f), 0.3f);

            await silverOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            redOnMainKitchen.LookAt(silversEyes);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            ChangeRedsSprites(calmBody, calmLeftEye, calmRightEye);
            redOnMainKitchen.transform.DOShakeScale(0.1f, -0.1f);
            await redOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await redOnMainKitchen.Say(NextLine);
            await silverOnMainKitchen.Say(NextLine);
            parentMainKitchenScene.SetActive(false);
        }

        private async Task HandshakeScene()
        {
            handshakeScene.SetActive(true);
            await redHandshake.Say(NextLine);
            await silverHandshake.Say(NextLine);
            await silverHandshake.Say(NextLine);
            await silverHandshake.Say(NextLine);
            await redHandshake.Say(NextLine);
            await redHandshake.Say(NextLine);
            await redHandshake.Say(NextLine);
            await silverHandshake.Say(NextLine);
            await silverHandshake.Say(NextLine);
            await silverHandshake.Say(NextLine);
            await redHandshake.Say(NextLine);
            await silverHandshake.Say(NextLine);
            await silverHandshake.Say(NextLine);
            await redHandshake.Say(NextLine);
            await silverHandshake.Say(NextLine);
            await silversHandshakePaw.DOMove(new Vector3(-7.27f, 5), 1f).AsyncWaitForCompletion();
            await silverHandshake.Say(NextLine);
            await redHandshake.Say(NextLine);
            await redsHandshakePaw.DOMove(new Vector3(6.66f, 4.78f, 0), 0.15f).AsyncWaitForCompletion();
            hadshakeParent.transform.DOShakeScale(0.1f, 0.25f);
            handshakeImpactSound.Play();
            music.volume = 1;
            await Task.Delay(750);
            theGreatestAdventureBeginsLogo.SetActive(true);
            await storyTeller.Say(NextLine);
        }

        private async Task DotweenStep(Transform steppingTransform, Vector3 targetPosition, float time)
            => await DotweenStep(steppingTransform, targetPosition, new Vector3(1.1f, 0.75f), time);

        private async Task DotweenStep(Transform steppingTransform, Vector3 targetPosition, Vector3 squeezeValue, float time)
        {
            steppingTransform.DOMove(targetPosition, time);
            await steppingTransform.DOScale(squeezeValue, time * 0.5f).AsyncWaitForCompletion();
            await steppingTransform.DOScale(Vector3.one, time * 0.5f).AsyncWaitForCompletion();
        }

        private async Task DotweenSteps(Transform steppingTransform, Vector3 targetPosition, Vector3 squeezeValue, float time, int stepsAmount)
        {
            var startPosition = steppingTransform.position;

            for (int i = 1; i <= stepsAmount; i++)
            {
                var intermediateTargetPosition =
                    Vector3.Lerp(startPosition, targetPosition, (float)i / stepsAmount);

                await DotweenStep(
                    steppingTransform,
                    intermediateTargetPosition,
                    squeezeValue,
                    time / stepsAmount);
            }
        }

        private void ChangeRedsSprites(Sprite bodySprite, Sprite leftEyeSprite, Sprite rightEyeSprite)
        {
            body.sprite = bodySprite;
            leftEye.sprite = leftEyeSprite;
            leftEyeMask.sprite = leftEyeSprite;
            rightEye.sprite = rightEyeSprite;
            rightEyeMask.sprite = rightEyeSprite;
        }
    }
}