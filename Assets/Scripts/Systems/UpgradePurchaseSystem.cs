using System;
using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class UpgradePurchaseSystem : IEcsRunSystem
    {
        public static event Action<string, string> OnBusinessUpgraded;
        
        private EcsFilter<BusinessComponent, UpgradesComponent, UpgradePurchaseComponent> _upgradeFilter;
        
        public void Run()
        {
            foreach (var i in _upgradeFilter)
            {
                var businessId = _upgradeFilter.Get1(i).Id;
                var upgradeStates = _upgradeFilter.Get2(i).UpgradeStates;
                var purchasedUpgrade = _upgradeFilter.Get3(i).Id;
                
                if (!upgradeStates.ContainsKey(purchasedUpgrade))
                    continue;

                upgradeStates[purchasedUpgrade] = true;
                
                OnBusinessUpgraded?.Invoke(businessId, purchasedUpgrade);
            }
        }
    }
}