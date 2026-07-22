using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace foxRestaurant
{
    public class RiversideScenarioPart8 : BaseScenario<RestaurantEncounter>
    {
        [SerializeField] private Character red;
        [SerializeField] private Character adele;
        [SerializeField] private AudioSource successSound;
        [SerializeField] private AudioSource impactSound;
        [SerializeField] private AudioSource music;

        [Header("Customers")]
        [SerializeField] private CustomerData hog;
        [SerializeField] private CustomerData cow;
        [SerializeField] private CustomerData goat;
        [SerializeField] private CustomerData bull;

        [Header("Dishes Data")]
        [SerializeField] private ItemData mushroomSoup;
        [SerializeField] private ItemData chickenSoup;
        [SerializeField] private ItemData mushroomPotRoast;
        [SerializeField] private ItemData chickenPotRoast;

        [Header("Ingredients Data")]
        [SerializeField] private ItemData mushroom;
        [SerializeField] private ItemData chicken;
        [SerializeField] private ItemData potRoast;
        [SerializeField] private ItemData soup;
        [SerializeField] private ItemData tea;
        [SerializeField] private ItemData compote;
        [SerializeField] private ItemData pretzel;
        [SerializeField] private ItemData bigCoal;
        [SerializeField] private ItemData coal;

        [Header("SeatPlaces")]
        [SerializeField] private Table table;

        private RestaurantEncounter encounter;

        protected override void InitTyped(RestaurantEncounter encounter)
        {
            this.encounter = encounter;
        }

        protected override async UniTask StartScenarioTyped(RestaurantEncounter encounter)
        {
            await UniTask.Delay(1000);

            await encounter.CurrentWaveManager.DoWaveTillComplete(new WaveConfig()
            {
                BeforeWave = new Func<UniTask>[]
                {
                    () => encounter.ItemsOperations.SpawnStartItems()
                },

                Customers = new List<QueuedCustomer>
                {
                    new(bull),
                    new(goat),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(goat),
                    new(hog),
                    new(cow),
                    new(hog),
                    new(cow),
                }
            });
        }
    }
}