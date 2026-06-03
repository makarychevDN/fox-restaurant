using UnityEngine;

namespace foxRestaurant
{
    public class GameplayPauseMenu : Menu
    {
        [SerializeField] private Level level;

        protected override void InitTitleMenuPanel()
        {
            base.InitTitleMenuPanel();
            titleMenuPanel.ResumeButton.onClick.AddListener(level.ClosePauseMenu);
        }
    }
}