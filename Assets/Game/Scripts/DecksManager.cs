using UnityEngine;

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

        public void Init()
        {
            ingredientsDeck = new Deck<ItemData>(ingredientsToSpawnAsset.DataList);
            dishesDeck = new Deck<ItemData>(dishesToOrderAsset.DataList);
            customersDeck = new Deck<CustomerData> (customersToSpawnAsset.DataList);
        }

        public ItemData GetRandomIngredient() => ingredientsDeck.Draw();
        public ItemData GetRandomDish() => dishesDeck.Draw();
        public CustomerData GetRandomCustomerh() => customersDeck.Draw();
    }
}