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
            .Add(new LvlUpSystem())
            .Add(new RevenueProgressSystem())
            .Add(new RevenuePaymentSystem())
            .Add(new BalanceSystem())
            .OneFrame<UpgradePurchaseComponent>()
            .OneFrame<LvlUpComponent>()
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
}