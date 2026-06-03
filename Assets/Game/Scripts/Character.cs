using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using static foxRestaurant.DialogueDisplayer;

namespace foxRestaurant
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private List<LookForCursorController> eyesAndNose;

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public async UniTask Say(string text)
        {
            await dialogueManager.DisplayDialogueLine(text);
            await UniTask.Yield();
        }

        public async UniTask Say(LocalizedString text)
        {
            await dialogueManager.DisplayDialogueLine(text.GetLocalizedString());
            await UniTask.Yield();
        }

        public void LookAt(Transform target)
        {
            eyesAndNose.ForEach(item => item.SetTarget(target));
        }

        public void SetDialoguePopUpCentering(Centering centering)
        {
            dialogueManager.SetDialoguePopUpCentering(centering);
        }

        public void SetDialoguePopUpLocalPosition(Vector3 newLocalPosition)
        {
            dialogueManager.SetDialoguePopUpLocalPosition(newLocalPosition);
        }

        public UnityEvent<string> OnEmote => dialogueManager.OnEmote;
    }
}