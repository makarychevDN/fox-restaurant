using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "ItemsDataList", menuName = "Scriptable Objects/ItemsDataList")]
    public class ItemsDataList : ScriptableObject
    {
        [field: SerializeField] public List<ItemData> DataList { get; private set; }
    }
}