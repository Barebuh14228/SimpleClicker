using System.Collections.Generic;
using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;
using UI;

namespace Systems
{
    public class BusinessCreationSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<Business, RevenueProgress, PurchasedUpgrades> _businessFilter;
        
        private GameSettings _gameSettings;
        private BusinessViewParent _businessViewParent;
        
        public void Init()
        {
            if (_businessFilter.IsEmpty())
            {
                CreateDefaultBusiness();
            }

            foreach (var i in _businessFilter)
            {
                var business = _businessFilter.Get1(i);
                var revenueProgress = _businessFilter.Get2(i);

                var settings = _gameSettings.BusinessSettingsList.First(bs => bs.Id == business.Id);
                
                _businessViewParent
                    .InstantiateNewBusinessView()
                    .Setup(
                        business.Id, 
                        business.Level, 
                        revenueProgress.Progress,
                        settings.Upgrade1Settings,
                        settings.Upgrade2Settings
                        );
                
                _businessFilter.GetEntity(i).Replace(new NewComponent());
            }
        }

        private void CreateDefaultBusiness()
        {
            var firstCreated = false;
        
            foreach (var bs in _gameSettings.BusinessSettingsList)
            {
                _world.NewEntity()
                    .Replace(new Business() { Id = bs.Id, Level = firstCreated ? 0 : 1 })
                    .Replace(new RevenueProgress())
                    .Replace(new RevenueModifier())
                    .Replace(new PurchasedUpgrades() { UpgradeIds = new List<string>()});
                
                firstCreated = true;
            }
        }
    }
}