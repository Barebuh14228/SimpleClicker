using System.Collections.Generic;
using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class UpgradePurchaseSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Business> _businessFilter;
        private EcsFilter<UpgradeRequest> _requestFilter;
        
        private GameSettings _gameSettings;
        
        public void Run()
        {
            foreach (var i in _requestFilter)
            {
                var businessId = _requestFilter.Get1(i).BusinessId;
                var upgradeId = _requestFilter.Get1(i).UpgradeId;

                var businessSettings = _gameSettings.BusinessSettingsList.First(bs => bs.Id == businessId);
                var upgradesList = new List<UpgradeSettings>()
                    { businessSettings.Upgrade1Settings, businessSettings.Upgrade2Settings };
                
                var price = upgradesList.First(us => us.Id == upgradeId).Price;
                
                foreach (var j in _businessFilter)
                {
                    if (_businessFilter.Get1(j).Id != businessId)
                        continue;
                    
                    _businessFilter.GetEntity(j).Replace(new Upgrade { Id = upgradeId } );
                    _world.NewEntity().Replace(new AddBalance() { Value = -price } );
                }
            }
        }
    }
}