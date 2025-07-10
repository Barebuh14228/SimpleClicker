using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class BalanceCreationSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<Balance> _balanceFilter;
        
        private GameSettings _gameSettings;
        
        public void Init()
        {
            if (_balanceFilter.IsEmpty())
            {
                _world.NewEntity().Replace(new Balance()
                {
                    Value = _gameSettings.DefaultBalance
                });
            }

            foreach (var i in _balanceFilter)
            {
                _balanceFilter.GetEntity(i).Replace(new NewComponent());
            }
        }
    }
}