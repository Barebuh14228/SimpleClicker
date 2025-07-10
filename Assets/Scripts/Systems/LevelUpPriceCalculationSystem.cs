using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class LevelUpPriceCalculationSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessComponent, LevelUpComponent> _levelUpFilter;
        private EcsFilter<BusinessComponent, New> _newFilter;
        
        private BusinessSettingsList _settingsList;
        
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

        private void CalculateLevelUpPrice(BusinessComponent businessComponent)
        {
            var id = businessComponent.Id;
            var level = businessComponent.Level;
            var settings = _settingsList.SettingsList.First(bs => bs.Id == id);

            var levelUpPrice = settings.BasePrice * (level + 1);
            
            GameController.NotifyLevelUpPriceChanged(id, levelUpPrice);
        }
    }
}