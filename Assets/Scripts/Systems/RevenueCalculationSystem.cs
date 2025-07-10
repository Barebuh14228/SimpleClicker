using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class RevenueCalculationSystem : IEcsRunSystem
    {
        private EcsFilter<Business, RevenueModifier, LevelUp> _levelUpFilter;
        private EcsFilter<Business, RevenueModifier, Upgrade> _upgradeFilter;
        private EcsFilter<Business, RevenueModifier, NewComponent> _newFilter;
        
        private GameSettings _gameSettings;
        
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

        private void CalculateRevenue(Business business, RevenueModifier modifier)
        {
            var id = business.Id;
            var level = business.Level;
            var settings = _gameSettings.BusinessSettingsList.First(bs => bs.Id == id);

            var revenue = level * settings.BaseRevenue * (1 + modifier.Value);
            
            GameController.NotifyBusinessRevenueChanged(id, revenue);
        }
    }
}