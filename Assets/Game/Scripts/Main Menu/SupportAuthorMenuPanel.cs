using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace foxRestaurant
{
    public class SupportAuthorMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button patreonButton;
        [SerializeField] private Button usdcSolanaButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button emailButton;
        [SerializeField] private DynamicTextManager DynamicTextManager;
        [SerializeField] private LocalizedString copiedText;
        public Button BackButton => backButton;

        private void Start()
        {
            patreonButton.onClick.AddListener(OpenPatreonLink);
            usdcSolanaButton.onClick.AddListener(CopySolanaAdress);
            emailButton.onClick.AddListener(CopyEmail);
        }

        private void OpenPatreonLink()
        {
            Application.OpenURL("https://www.patreon.com/cw/RedtheCook");
        }

        private void CopySolanaAdress()
        {
            GUIUtility.systemCopyBuffer = "8gDxUbYjtn225eJaNGJh47xHBuJScEDu8xeL4tBxgJRN";
            DynamicTextManager.SpawnDynamicText(usdcSolanaButton.transform.position, copiedText.GetLocalizedString(), Color.white, usdcSolanaButton.transform.position + Vector3.up * 1);
        }

        private void CopyEmail()
        {
            GUIUtility.systemCopyBuffer = "gamesbytraust@gmail.com";
            DynamicTextManager.SpawnDynamicText(emailButton.transform.position, copiedText.GetLocalizedString(), Color.white, emailButton.transform.position + Vector3.up * 1);
        }
    }
}