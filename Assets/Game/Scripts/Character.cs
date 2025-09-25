using System.Threading.Tasks;
using UnityEngine;

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

        public async Task SayAsync(string text)
        {
            await dialogueManager.DisplayDialogueLine(text);
        }
    }
}