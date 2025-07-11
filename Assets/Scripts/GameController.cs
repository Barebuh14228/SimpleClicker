using System;
using Components;

public static class GameController
{
    public static event Action<float> OnBalanceChanged;
    public static event Action<string, int> OnBusinessLevelUp;
    public static event Action<string, float> OnRevenueProgressChanged;
    public static event Action<string, string> OnBusinessUpgraded;
    public static event Action<string, float> OnBusinessRevenueChanged;
    public static event Action<string, float> OnLevelUpPriceChanged;
    
    private static GameModel _model;

    static GameController()
    {
        _model = new GameModel();
    }
    
    public static void InitModel()
    {
        _model.Init();
    }

    public static void RunModel()
    {
        _model.Run();
    }

    public static void DestroyModel()
    {
        _model.Destroy();
    }

    public static void InjectInModel<T>(T injection)
    {
        _model.Inject(injection);
    }

    public static void NotifyBalanceChanged(float balance)
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

    public static void NotifyBusinessRevenueChanged(string businessId, float revenue)
    {
        OnBusinessRevenueChanged?.Invoke(businessId, revenue);
    }

    public static void NotifyLevelUpPriceChanged(string businessId, float price)
    {
        OnLevelUpPriceChanged?.Invoke(businessId, price);
    }

    public static void SendLevelUpRequest(string businessId)
    {
        _model.SendRequest(new LevelUpRequest()
        {
            BusinessId = businessId
        });
    }

    public static void SendUpgradeRequest(string businessId, string upgradeId)
    {
        _model.SendRequest(new UpgradeRequest()
        {
            BusinessId = businessId,
            UpgradeId = upgradeId
        });
    }

    public static void SaveModel()
    {
        _model.Save();
    }
}