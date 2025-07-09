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
        public static event Action<string, float> OnRevenueProgressChanged;
        
        private EcsFilter<BusinessComponent, RevenueProgressComponent> _filter;

        private BusinessSettingsList _settingsList;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                if (_filter.Get1(i).Level < 1)
                    continue;
                
                var business = _filter.Get1(i);
                ref var revenueProgress = ref _filter.Get2(i);
                
                var delay = _settingsList.SettingsList.First(bs => bs.Id == business.Id).Delay;

                var addProgress = Time.deltaTime / delay;
                
                revenueProgress.Progress += addProgress;
                
                if (revenueProgress.Progress >= 1)
                {
                    revenueProgress.Progress = 0;
                    _filter.GetEntity(i).Replace(new PaymentComponent());
                }
                
                OnRevenueProgressChanged?.Invoke(business.Id, revenueProgress.Progress);
            }
        }
    }
}