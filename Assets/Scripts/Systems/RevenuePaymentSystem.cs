using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class RevenuePaymentSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Business, RevenueModifier, Revenue> _paymentFilter;
        
        private GameSettings _gameSettings;
        
        public void Run()
        {
            foreach (var i in _paymentFilter)
            {
                var business = _paymentFilter.Get1(i);
                var modifier = 1 + _paymentFilter.Get2(i).Value;
                var settings = _gameSettings.BusinessSettingsList.First(bs => bs.Id == business.Id);

                var revenue = business.Level * settings.BaseRevenue * modifier;

                _world.NewEntity().Replace(new AddBalance() { Value = revenue });
            }
        }
    }
}