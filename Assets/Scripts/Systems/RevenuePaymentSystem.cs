using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class RevenuePaymentSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<BusinessComponent, UpgradesComponent, PaymentComponent> _paymentFilter;
        
        private BusinessSettingsList _settingsList;
        
        public void Run()
        {
            foreach (var i in _paymentFilter)
            {
                var business = _paymentFilter.Get1(i);
                var upgrades = _paymentFilter.Get2(i);

                var settings = _settingsList.SettingsList.First(bs => bs.Id == business.Id);
                var modifier = 1 + upgrades.UpgradeStates
                    .Where(kv => kv.Value)
                    .Select(kv => kv.Key)
                    .Select(id => settings.UpgradesList.First(us => us.Id == id))
                    .Sum(us => us.Modifier);

                var revenue = business.Level * settings.BaseRevenue * modifier;

                _world.NewEntity().Replace(new ChangeBalanceComponent() { Value = (int) revenue });
            }
        }
    }
}