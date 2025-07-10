using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class BalanceSystem : IEcsRunSystem
    {
        private EcsFilter<BalanceComponent> _balanceFilter;
        private EcsFilter<ChangeBalanceComponent> _changeBalanceFilter;
        
        public void Run()
        {
            var balanceDiff = 0f;

            foreach (var i in _changeBalanceFilter)
            {
                balanceDiff += _changeBalanceFilter.Get1(i).Value;
            }
            
            if (balanceDiff == 0)
                return;

            foreach (var i in _balanceFilter)
            {
                _balanceFilter.Get1(i).Value += balanceDiff;
                
                GameController.NotifyBalanceChanged(_balanceFilter.Get1(i).Value);
            }
        }
    }
}