using UnityEngine;

namespace foxRestaurant
{
    public class TimeScaleSetter : MonoBehaviour
    {
        public void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }
    }
}