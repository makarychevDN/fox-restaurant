using System.Collections.Generic;
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
        private List<Button> hourGlassButtons;

        public void Init(RestaurantEncounter restaurantEncounter)
        {
            recipeBook.Init(restaurantEncounter);

            hourGlassButtons = new List<Button>() { pauseButton, playButton, playX2Button, playX4Button };
            pauseButton.onClick.AddListener(() => PauseButtonPressedHandler(restaurantEncounter));
            playButton.onClick.AddListener(() => PlayButtonPressedHandler(restaurantEncounter));
            playX2Button.onClick.AddListener(() => X2ButtonPressedHandler(restaurantEncounter));
            playX4Button.onClick.AddListener(() => X4ButtonPressedHandler(restaurantEncounter));
        }

        private void PauseButtonPressedHandler(RestaurantEncounter restaurantEncounter)
        {
            restaurantEncounter.Ticker.Pause();
            UpdateButtonsStates(pauseButton);
        }

        private void PlayButtonPressedHandler(RestaurantEncounter restaurantEncounter)
        {
            restaurantEncounter.Ticker.SetRegularTickingSpeed();
            UpdateButtonsStates(playButton);
        }

        private void X2ButtonPressedHandler(RestaurantEncounter restaurantEncounter)
        {
            restaurantEncounter.Ticker.SetX2TickingSpeed();
            UpdateButtonsStates(playX2Button);
        }

        private void X4ButtonPressedHandler(RestaurantEncounter restaurantEncounter)
        {
            restaurantEncounter.Ticker.SetX4TickingSpeed();
            UpdateButtonsStates(playX4Button);
        }

        private void UpdateButtonsStates(Button disabledButton)
        {
            hourGlassButtons.ForEach(button => button.interactable = (button != disabledButton));
        }
    }
}