using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foxRestaurant
{
    public class DynamicTextManager : MonoBehaviour
    {
        [SerializeField] private Transform textsParent;
        [SerializeField] private DynamicText dinamicTextPrefab;
        private List<DynamicText> spawnedDynamicTextList = new();
        private Dictionary<ReservedColors, Color> colorsDictionary = new()
        {
            {ReservedColors.YellowUI, Extensions.HexToColor("#d3c82a") }
        };

        public void SpawnDynamicText(Vector3 canvasPosition, string text, Color color)
        {
            var spawnedObject = spawnedDynamicTextList.FirstOrDefault(x => !x.gameObject.activeSelf);

            if (spawnedObject == null)
                spawnedObject = InstantiateText();

            spawnedObject.gameObject.SetActive(true);
            spawnedObject.transform.position = canvasPosition;
            spawnedObject.Init(text, color);
        }

        public void SpawnDynamicText(Vector3 canvasPosition, string text, ReservedColors reservedColor) =>
            SpawnDynamicText(canvasPosition, text, colorsDictionary[reservedColor]);

        private DynamicText InstantiateText()
        {
            var spawnedObject = Instantiate(dinamicTextPrefab);
            spawnedObject.transform.parent = textsParent;
            spawnedDynamicTextList.Add(spawnedObject);
            return spawnedObject;
        }
    }

    public enum ReservedColors
    {
        YellowUI = 0,
        GreenUI = 10,
        OrangeUI = 20
    }
}