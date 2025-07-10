using Components;
using Leopotam.Ecs;
using Systems;

public class GameModel
{
    private EcsWorld _world;
    private EcsSystems _systems;

    public GameModel()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world)
            .Add(new MainInitSystem())
            .Add(new UpgradePurchaseSystem())
            .Add(new UpgradeSystem())
            .Add(new LevelUpPurchaseSystem())
            .Add(new LevelUpSystem())
            .Add(new RevenueCalculationSystem())
            .Add(new LevelUpPriceCalculationSystem())
            .Add(new RevenueProgressSystem())
            .Add(new RevenuePaymentSystem())
            .Add(new BalanceSystem())
            .OneFrame<BusinessUpgradeRequest>()
            .OneFrame<UpgradeComponent>()
            .OneFrame<BusinessLevelUpRequest>()
            .OneFrame<LevelUpComponent>()
            .OneFrame<PaymentComponent>()
            .OneFrame<ChangeBalanceComponent>();
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