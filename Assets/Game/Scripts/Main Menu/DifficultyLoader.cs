using UnityEngine;

namespace foxRestaurant
{
    public class DifficultyLoader : MonoBehaviour
    {
        private void Awake()
        {
            GameSettings.Difficulty = (Difficulty)PlayerPrefs.GetInt("Difficulty", (int)Difficulty.None);
            print($"Difficulty loaded: {GameSettings.Difficulty}");
        }

        [ContextMenu("Delete Difficulty")]
        public void DeleteDifficulty()
        {
            PlayerPrefs.DeleteKey("Difficulty");
            print("Difficulty deleted");
        }
    }
}