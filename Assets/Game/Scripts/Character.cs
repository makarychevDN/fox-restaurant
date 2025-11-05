using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

namespace foxRestaurant
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private DialogueManager dialogueManager;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public async Task Say(string text)
        {
            await dialogueManager.DisplayDialogueLine(text);
        }

        public async Task Say(LocalizedString text)
        {
            await dialogueManager.DisplayDialogueLine(text.GetLocalizedString());
        }
    }
}