using UnityEngine;

namespace foxRestaurant
{
    public class RecipeBlackBoard : UIRepeater<RecipePanel, Recipe>
    {
        [SerializeField] private Animator animator;

        public void Appear() => animator.SetBool("Appeared", true);
        public void Disappear() => animator.SetBool("Appeared", false);

        public void Init(Level level)
        {
            level.OnDataBaseUpdated.AddListener(UpdatePanels);
        }

        /// <summary>
        /// Реализация привязки Recipe -> RecipePanel
        /// </summary>
        protected override void Bind(RecipePanel panel, Recipe data)
        {
            panel.SetRecipe(data);
        }

        public void UpdatePanels(DataBase dataBase)
        {
            UpdatePanels(dataBase.Recipes); // вызываем базовый UpdatePanels
        }
    }
}