using Components;
using Leopotam.Ecs;
using Systems;

//todo list

//codestyle
//refactor

public class GameModel
{
    private EcsWorld _world;
    private EcsSystems _systems;

    public GameModel()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world)
            .Add(new LoadSystem())
            .Add(new EntitiesRestoreSystem())
            .Add(new BalanceCreationSystem())
            .Add(new BusinessCreationSystem())
            .Add(new UpgradePurchaseSystem())
            .Add(new UpgradeSystem())
            .Add(new LevelUpPurchaseSystem())
            .Add(new LevelUpSystem())
            .Add(new RevenueCalculationSystem())
            .Add(new LevelUpPriceCalculationSystem())
            .Add(new RevenueProgressSystem())
            .Add(new RevenuePaymentSystem())
            .Add(new BalanceSystem())
            .Add(new SaveSystem())
            .OneFrame<UpgradeRequest>()
            .OneFrame<Upgrade>()
            .OneFrame<LevelUpRequest>()
            .OneFrame<LevelUp>()
            .OneFrame<Revenue>()
            .OneFrame<AddBalance>()
            .OneFrame<NewComponent>()
            .OneFrame<LoadedData>();
    }
    
    public void Init()
    {
        _systems.Init();
    }

    public void Run()
    {
        _systems.Run();
    }

    public void Destroy()
    {
        _systems.Destroy();
        _world.Destroy();
    }

    public void Inject<T>(T injection)
    {
        _systems.Inject(injection);
    }

    public void SendRequest<T>(T request) where T : struct
    {
        _world.NewEntity().Replace(request);
    }
}