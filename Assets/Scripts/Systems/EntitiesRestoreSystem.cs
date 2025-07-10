using Components;
using Leopotam.Ecs;

namespace Systems
{
    public class EntitiesRestoreSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private EcsFilter<LoadedData> _dataFilter;
        
        public void Init()
        {
            foreach (var i in _dataFilter)
            {
                var data = _dataFilter.Get1(i).SaveData;

                _world.NewEntity().Replace(data.Balance);

                foreach (var businessSaveData in data.BusinessEntities)
                {
                    _world.NewEntity()
                        .Replace(businessSaveData.Business)
                        .Replace(businessSaveData.RevenueProgress)
                        .Replace(businessSaveData.RevenueModifier)
                        .Replace(businessSaveData.PurchasedUpgrades);
                }
            }
        }
        
        
    }
}