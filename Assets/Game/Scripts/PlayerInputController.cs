using UnityEngine;
using UnityEngine.UI;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Button spawnIngredientButton;

    public void Init(Level level)
    {
        spawnIngredientButton.onClick.AddListener(level.ItemsSpawner.ActivateNewSlot);
    }
}
