using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class UpgradeSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessComponent, UpgradeStatesComponent, UpgradeComponent> _upgradeFilter;
        
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
                
                GameController.NotifyBusinessUpgraded(businessId, purchasedUpgrade);
            }
        }
    }
}