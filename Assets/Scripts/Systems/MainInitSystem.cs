using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;
using UI;

namespace Systems
{
    public class MainInitSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private BusinessSettingsList _settingsList;
        private BusinessViewParent _businessViewParent;
        
        public void Init()
        {
            //todo default balance
            
            _world.NewEntity()
                .Replace(new BalanceComponent())
                .Replace(new ChangeBalanceComponent() { Value = 1000 });
        
            var firstset = false;
        
            foreach (var bs in _settingsList.SettingsList)
            {
                var lvl = !firstset ? 1 : 0;
            
                var entity = _world.NewEntity()
                    .Replace(new BusinessComponent() { Id = bs.Id})
                    .Replace(new RevenueProgressComponent())
                    .Replace(new UpgradeStatesComponent() { UpgradeStates = bs.UpgradesList.Select(us => us.Id).ToDictionary(id => id, id => false) });
                
                //todo add UpgradeComponent on load
                
                if (!firstset)
                {
                    entity.Replace(new LevelUpComponent());
                    firstset = true;
                }
                else
                {
                    entity.Replace(new New());
                }
                
                _businessViewParent.InstantiateNewBusinessView().Setup(bs, 0, lvl);
            }
        }
    }
}