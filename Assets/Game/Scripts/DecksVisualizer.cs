using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class DecksVisualizer : MonoBehaviour
    {
        [SerializeField] private Image segmentPrefab;
        [SerializeField] private Transform ordersDeckParent;
        [SerializeField] private Transform ingridientsDeckParent;
        [SerializeField] private DecksManager decksManager;
        [SerializeField] private Sprite randomSprite;

        private List<Image> ordersDeckSegments = new List<Image>();
        private List<Image> ingredientsDeckSegments = new List<Image>();

        private void Awake()
        {
            decksManager.OnDishesToOrderDeckUpdated.AddListener(UpdateOrdersDeck);
            decksManager.OnIngridientsDeckUpdated.AddListener(UpdateIngredientsDeck);
        }

        private void UpdateOrdersDeck()
        {
            UpdateDeckVisualizer(ordersDeckSegments, () => decksManager.Dishes, ordersDeckParent);
        }

        private void UpdateIngredientsDeck()
        {
            UpdateDeckVisualizer(ingredientsDeckSegments, () => decksManager.Ingredients, ingridientsDeckParent);
        }

        private void UpdateDeckVisualizer(List<Image> deckSegments, Func<List<ItemData>> getDeckData, Transform segmentsParent)
        {

            var deckData = getDeckData();

            while (deckSegments.Count <= deckData.Count)
            {
                var segment = Instantiate(segmentPrefab, segmentsParent);
                deckSegments.Add(segment);
                print($"Added new segment for {segmentsParent.name}. Total segments: {deckSegments.Count}");
            }

            for (int i = 0; i < deckSegments.Count; i++)
            {
                if (deckData.Count > i)
                {
                    print("Deck data count is less than index, sprite is assigned.");
                    deckSegments[i].sprite = deckData[i].Sprite;
                }

                deckSegments[i].gameObject.SetActive(deckData.Count > i);
            }
        }
    }
}