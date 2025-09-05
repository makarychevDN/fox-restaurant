using System.Collections.Generic;
using UnityEngine;

namespace foxRestaurant
{
    [CreateAssetMenu(fileName = "CustomersDataList", menuName = "Scriptable Objects/CustomersDataList")]
    public class CustomersDataList : ScriptableObject
    {
        [field: SerializeField] public List<CustomerData> DataList { get; private set; }
    }
}
