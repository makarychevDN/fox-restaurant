using DG.Tweening;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class CustomersBuffsDisplayer : MonoBehaviour
    {
        [Header("hunger points increased buff")]
        [SerializeField] private Animator hungerPointsIncreasedBuff;
        [SerializeField] private AudioSource hungerPointsIncreasedSound;

        public void Init(Customer customer)
        {
            customer.OnHungerPointsIncreased.AddListener(DisplayHungerPointsIncreasedBuff);
        }

        public async void DisplayHungerPointsIncreasedBuff()
        {
            hungerPointsIncreasedSound.Play();
            hungerPointsIncreasedBuff.SetTrigger("hp buffed");
        }
    }
}