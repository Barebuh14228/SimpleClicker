using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class LevelUpPriceCalculationSystem : IEcsRunSystem
    {
        private EcsFilter<Business, LevelUp> _levelUpFilter;
        private EcsFilter<Business, NewComponent> _newFilter;
        
        private GameSettings _gameSettings;
        
        public void Run()
        {
            foreach (var i in _levelUpFilter)
            {
                CalculateLevelUpPrice(_levelUpFilter.Get1(i));
            }
            
            foreach (var i in _newFilter)
            {
                CalculateLevelUpPrice(_newFilter.Get1(i));
            }
        }

        private void CalculateLevelUpPrice(Business business)
        {
            var id = business.Id;
            var level = business.Level;
            var settings = _gameSettings.BusinessSettingsList.First(bs => bs.Id == id);

            var levelUpPrice = settings.BasePrice * (level + 1);
            
            GameController.NotifyLevelUpPriceChanged(id, levelUpPrice);
        }
    }
}