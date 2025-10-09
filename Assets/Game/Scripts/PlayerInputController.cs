using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private RecipeBook recipeBook;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button playX2Button;
        [SerializeField] private Button playX4Button;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            recipeBook.Init(restaurantEncounter);
            playButton.onClick.AddListener(restaurantEncounter.Ticker.SetRegularTickingSpeed);
            playX2Button.onClick.AddListener(restaurantEncounter.Ticker.SetX2TickingSpeed);
            playX4Button.onClick.AddListener(restaurantEncounter.Ticker.SetX4TickingSpeed);
            pauseButton.onClick.AddListener(restaurantEncounter.Ticker.Pause);
        }
    }
}