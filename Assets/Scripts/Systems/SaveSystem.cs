using System;
using System.Collections.Generic;
using System.IO;
using Components;
using Leopotam.Ecs;
using Save;
using UnityEngine;

namespace Systems
{
    public class SaveSystem : IEcsDestroySystem
    {
        private EcsWorld _world;
        private EcsFilter<Balance> _balanceFilter;
        private EcsFilter<Business, RevenueProgress, RevenueModifier, PurchasedUpgrades> _businessFilter;
        
        public void Destroy()
        {
            var saveData = new SaveData();

            foreach (var i in _balanceFilter)
            {
                saveData.Balance = _balanceFilter.Get1(i);
            }

            foreach (var i in _businessFilter)
            {
                saveData.BusinessEntities ??= new List<BusinessSaveData>();
                saveData.BusinessEntities.Add(new BusinessSaveData()
                {
                    Business = _businessFilter.Get1(i),
                    RevenueProgress = _businessFilter.Get2(i),
                    RevenueModifier = _businessFilter.Get3(i),
                    PurchasedUpgrades = _businessFilter.Get4(i)
                });
            }
            
            var json = JsonUtility.ToJson(saveData);
            
            File.WriteAllText(App.SavePath, json);
            
            Debug.Log($"<color=yellow>Game saved at {App.SavePath}</color>");
        }
    }
}