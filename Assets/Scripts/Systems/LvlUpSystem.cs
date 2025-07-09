using System;
using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class LvlUpSystem : IEcsRunSystem
    {
        public static event Action<string, int> OnBusinessLvlUp;
        
        private EcsFilter<BusinessComponent, LvlUpComponent> _levelUpFilter;
        
        public void Run()
        {
            foreach (var i in _levelUpFilter)
            {
                ref var businessComponent = ref _levelUpFilter.Get1(i);
                
                businessComponent.Level++;
                
                OnBusinessLvlUp?.Invoke(businessComponent.Id, businessComponent.Level);
            }
        }
    }
}