using System;
using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;
using UnityEngine;

namespace Systems
{
    public class RevenueProgressSystem : IEcsRunSystem
    {
        private EcsFilter<Business, RevenueProgress> _filter;

        private GameSettings _gameSettings;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                if (_filter.Get1(i).Level < 1)
                    continue;
                
                var business = _filter.Get1(i);
                ref var revenueProgress = ref _filter.Get2(i);
                
                var delay = _gameSettings.BusinessSettingsList.First(bs => bs.Id == business.Id).Delay;

                var addProgress = Time.deltaTime / delay;
                
                revenueProgress.Progress += addProgress;
                
                if (revenueProgress.Progress >= 1)
                {
                    revenueProgress.Progress = 0;
                    _filter.GetEntity(i).Replace(new Revenue());
                }
                
                GameController.NotifyRevenueProgressChanged(business.Id, revenueProgress.Progress);
            }
        }
    }
}