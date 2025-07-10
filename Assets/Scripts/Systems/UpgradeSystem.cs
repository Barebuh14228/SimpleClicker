using System.Collections.Generic;
using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class UpgradeSystem : IEcsRunSystem
    {
        private EcsFilter<Business, PurchasedUpgrades, Upgrade, RevenueModifier> _businessFilter;
        private EcsFilter<Business, PurchasedUpgrades, NewComponent> _createdBusinessFilter;
        
        private GameSettings _gameSettings;
        
        public void Run()
        {
            foreach (var i in _businessFilter)
            {
                var businessId = _businessFilter.Get1(i).Id;
                var purchasedUpgrades = _businessFilter.Get2(i).UpgradeIds;
                var upgradeId = _businessFilter.Get3(i).Id;
                
                purchasedUpgrades.Add(upgradeId);
                
                var businessSettings = _gameSettings.BusinessSettingsList.First(bs => bs.Id == businessId);
                var upgradesList = new List<UpgradeSettings>()
                    { businessSettings.Upgrade1Settings, businessSettings.Upgrade2Settings };
                
                var modifier = upgradesList.First(us => us.Id == upgradeId).Modifier;
                
                _businessFilter.Get4(i).Value += modifier;
                
                GameController.NotifyBusinessUpgraded(businessId, upgradeId);
            }

            foreach (var i in _createdBusinessFilter)
            {
                var businessId = _createdBusinessFilter.Get1(i).Id;
                var purchasedUpgrades = _createdBusinessFilter.Get2(i).UpgradeIds;
                
                foreach (var id in purchasedUpgrades)
                {
                    GameController.NotifyBusinessUpgraded(businessId, id);
                }
            }
        }
    }
}