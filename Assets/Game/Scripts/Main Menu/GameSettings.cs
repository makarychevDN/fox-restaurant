using UnityEngine;

namespace foxRestaurant
{
    public static class GameSettings
    {
        public static Difficulty Difficulty { get; set; }

        public static int GetAdditionalPatienceForDifficulty()
        {
            return Difficulty switch
            {
                Difficulty.StoryMode => 60,
                Difficulty.Easy => 30,
                Difficulty.Normal => 15,
                Difficulty.Hard => 0,
                _ => 0
            };
        }
    }

    public enum Difficulty
    {
        None = 0,
        StoryMode = 10,
        Easy = 20,
        Normal = 30,
        Hard = 40
    }
}