using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    public class TransitionsBlackBoard : UIRepeater<PackOfTransitionsPanel, RootData>
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ItemsDataList rootIngredients;

        public void Appear() => animator.SetBool("Appeared", true);
        public void Disappear() => animator.SetBool("Appeared", false);

        public void Init(RestaurantEncounter level)
        {
            level.OnDataBaseUpdated.AddListener(UpdatePanels);
        }

        protected override void Bind(PackOfTransitionsPanel panel, RootData rootData)
        {
            panel.UpdatePanels(rootData);
        }

        public void UpdatePanels(DataBase dataBase)
        {
            List<RootData> list = new List<RootData>();
            foreach(var rootItem in rootIngredients.DataList)
            {
                list.Add(new RootData(rootItem, dataBase));
            }

            UpdatePanels(list);
        }
    }

    public class RootData
    {
        private ItemData rootItemData;
        private DataBase dataBase;

        public ItemData RootItemData => rootItemData;
        public DataBase DataBase => dataBase;

        public RootData(ItemData rootItemData, DataBase dataBase)
        {
            this.rootItemData = rootItemData;
            this.dataBase = dataBase;
        }
    }
}