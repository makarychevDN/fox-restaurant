using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace foxRestaurant
{
    public class DynamicTextManager : MonoBehaviour
    {
        [SerializeField] private Transform textsParent;
        [SerializeField] private DynamicText dinamicTextPrefab;
        private List<DynamicText> spawnedDynamicTextList = new();

        public void SpawnDynamicText(Vector3 canvasPosition, string text)
        {
            var spawnedObject = spawnedDynamicTextList.FirstOrDefault(x => !x.gameObject.activeSelf);

            if (spawnedObject == null)
                spawnedObject = InstantiateText();

            spawnedObject.gameObject.SetActive(true);
            spawnedObject.transform.position = canvasPosition;
            var color = Extensions.HexToColor("#d3c82a");
            spawnedObject.Init(text, color);
        }

        private DynamicText InstantiateText()
        {
            var spawnedObject = Instantiate(dinamicTextPrefab);
            spawnedObject.transform.parent = textsParent;
            spawnedDynamicTextList.Add(spawnedObject);
            return spawnedObject;
        }
    }
}