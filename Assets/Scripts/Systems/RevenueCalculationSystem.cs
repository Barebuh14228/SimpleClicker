using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class RevenueCalculationSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessComponent, UpgradeStatesComponent, LevelUpComponent> _levelUpFilter;
        private EcsFilter<BusinessComponent, UpgradeStatesComponent, UpgradeComponent> _upgradeFilter;
        private EcsFilter<BusinessComponent, UpgradeStatesComponent, New> _newFilter;
        
        private BusinessSettingsList _settingsList;
        
        public void Run()
        {
            foreach (var i in _levelUpFilter)
            {
                CalculateRevenue(_levelUpFilter.Get1(i), _levelUpFilter.Get2(i));
            }
            
            foreach (var i in _upgradeFilter)
            {
                CalculateRevenue(_upgradeFilter.Get1(i), _upgradeFilter.Get2(i));
            }
            
            foreach (var i in _newFilter)
            {
                CalculateRevenue(_newFilter.Get1(i), _newFilter.Get2(i));
            }
        }

        private void CalculateRevenue(BusinessComponent businessComponent, UpgradeStatesComponent upgradeStatesComponent)
        {
            var id = businessComponent.Id;
            var level = businessComponent.Level;
            var states = upgradeStatesComponent.UpgradeStates;
            var settings = _settingsList.SettingsList.First(bs => bs.Id == id);
            
            var modifier = 1 + states
                .Where(kv => kv.Value)
                .Select(kv => kv.Key)
                .Select(id => settings.UpgradesList.First(us => us.Id == id))
                .Sum(us => us.Modifier);

            var revenue = level * settings.BaseRevenue * modifier;
            
            GameController.NotifyBusinessRevenueChanged(id, revenue);
        }
    }
}