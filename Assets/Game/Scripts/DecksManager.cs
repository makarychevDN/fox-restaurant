using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace foxRestaurant
{
    public class DecksManager : MonoBehaviour
    {
        [SerializeField] private ItemsDataList ingredientsToSpawnAsset;
        [SerializeField] private ItemsDataList dishesToOrderAsset;
        [SerializeField] private CustomersDataList customersToSpawnAsset;

        private Deck<ItemData> ingredientsDeck;
        private Deck<ItemData> dishesDeck;
        private Deck<CustomerData> customersDeck;

        public UnityEvent OnIngridientsDeckUpdated;
        public UnityEvent OnDishesToOrderDeckUpdated;

        public List<ItemData> Ingredients => ingredientsDeck.Elements;
        public List<ItemData> Dishes => dishesDeck.Elements;

        public void Init()
        {
            ingredientsDeck = new Deck<ItemData>(ingredientsToSpawnAsset.DataList, 1);
            dishesDeck = new Deck<ItemData>(dishesToOrderAsset.DataList, 1);
            customersDeck = new Deck<CustomerData> (customersToSpawnAsset.DataList, 2);
        }

        public ItemData GetRandomIngredient()
        {
            var ingredient = ingredientsDeck.Draw();
            print(ingredientsDeck.Elements.Count);
            OnIngridientsDeckUpdated.Invoke();
            return ingredient;
        }

        public ItemData GetRandomDish()
        {
            var dish = dishesDeck.Draw();
            OnDishesToOrderDeckUpdated.Invoke();
            return dish;
        }

        public CustomerData GetRandomCustomer() => customersDeck.Draw();
        public ItemData DrawRandomDishExcept(ItemData except) => dishesDeck.DrawExcept(except);
        public ItemData RollRandomDishExcept(ItemData except) => dishesDeck.RollExcept(except);
    }
}