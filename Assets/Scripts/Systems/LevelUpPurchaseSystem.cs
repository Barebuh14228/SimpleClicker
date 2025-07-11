using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;

namespace Systems
{
    public class LevelUpPurchaseSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Business> _businessFilter;
        private EcsFilter<LevelUpRequest> _requestFilter;
        
        private GameSettings _gameSettings;
        
        public void Run()
        {
            foreach (var i in _requestFilter)
            {
                var businessId = _requestFilter.Get1(i).BusinessId;
                var settings = _gameSettings.BusinessSettingsList.First(bs => bs.Id == businessId);
                
                foreach (var j in _businessFilter)
                {
                    if (_businessFilter.Get1(j).Id != businessId)
                        continue;

                    var level = _businessFilter.Get1(j).Level;
                    var price = settings.BasePrice * (level + 1);
                    
                    _businessFilter.GetEntity(j).Replace(new LevelUp() );
                    _world.NewEntity().Replace(new AddBalance() { Value = -price });
                    
                    break;
                }
            }
        }
    }
}