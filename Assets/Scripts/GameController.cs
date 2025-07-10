using System;

public static class GameController
{
    public static event Action<int> OnBalanceChanged;
    public static event Action<string, int> OnBusinessLevelUp;
    public static event Action<string, float> OnRevenueProgressChanged;
    public static event Action<string, string> OnBusinessUpgraded;
    
    private static GameModel _model;

    static GameController()
    {
        _model = new GameModel();
    }
    
    public static void Init()
    {
        _model.Init();
    }

    public static void Run()
    {
        _model.Run();
    }

    public static void Destroy()
    {
        _model.Destroy();
    }

    public static void InjectInModel<T>(T injection)
    {
        _model.Inject(injection);
    }

    public static void NotifyBalanceChanged(int balance)
    {
        OnBalanceChanged?.Invoke(balance);
    }
    
    public static void NotifyBusinessLevelUp(string businessId, int level)
    {
        OnBusinessLevelUp?.Invoke(businessId, level);
    }
    
    public static void NotifyRevenueProgressChanged(string businessId, float progress)
    {
        OnRevenueProgressChanged?.Invoke(businessId, progress);
    }
    
    public static void NotifyBusinessUpgraded(string businessId, string upgradeId)
    {
        OnBusinessUpgraded?.Invoke(businessId, upgradeId);
    }
}