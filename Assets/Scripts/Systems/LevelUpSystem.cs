using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class LevelUpSystem : IEcsRunSystem
    {
        private EcsFilter<BusinessComponent, LevelUpComponent> _levelUpFilter;
        
        public void Run()
        {
            foreach (var i in _levelUpFilter)
            {
                ref var businessComponent = ref _levelUpFilter.Get1(i);
                
                businessComponent.Level++;
                
                GameController.NotifyBusinessLevelUp(businessComponent.Id, businessComponent.Level);
            }
        }
    }
}