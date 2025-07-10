using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class BalanceSystem : IEcsRunSystem
    {
        private EcsFilter<Balance> _balanceFilter;
        private EcsFilter<Balance, NewComponent> _createdBalanceFilter;
        private EcsFilter<AddBalance> _changeBalanceFilter;
        
        public void Run()
        {
            foreach (var i in _createdBalanceFilter)
            {
                GameController.NotifyBalanceChanged(_createdBalanceFilter.Get1(i).Value);
            }
            
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