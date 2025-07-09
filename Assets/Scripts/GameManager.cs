using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;
using Systems;
using UI;
using UnityEngine;

//todo list

//дефолтное значение балланса
//система создания бизнесов
//сохранение/загрузка
//текста
//верстка: дизейблер и оптимизация

public class GameManager : MonoBehaviour
{
    [SerializeField] private BusinessSettingsList _settingsList;
    [SerializeField] private BusinessView _businessViewPrefab;
    [SerializeField] private Transform _businessParent;
    [SerializeField] private BalanceView _balanceView;
    
    private EcsWorld _world;
    private EcsSystems _systems;

    void Start () 
    {
        _world = new EcsWorld ();
        _systems = new EcsSystems(_world)
            .Add(new UpgradePurchaseSystem())
            .Add(new LvlUpSystem())
            .Add(new RevenueProgressSystem())
            .Add(new RevenuePaymentSystem())
            .Add(new BalanceSystem())
            .OneFrame<UpgradePurchaseComponent>()
            .OneFrame<LvlUpComponent>()
            .OneFrame<PaymentComponent>()
            .OneFrame<ChangeBalanceComponent>()
            .Inject(_settingsList);

        _world.NewEntity().Replace(new BalanceComponent() { Value = 1000 });
        _balanceView.Setup(1000);
        var firstset = false;
        
        foreach (var bs in _settingsList.SettingsList)
        {
            var lvl = !firstset ? 1 : 0;
            
            var entity = _world.NewEntity()
                .Replace(new BusinessComponent() { Id = bs.Id, Level = lvl})
                .Replace(new RevenueProgressComponent())
                .Replace(new UpgradesComponent() { UpgradeStates = bs.UpgradesList.Select(us => us.Id).ToDictionary(id => id, id => false) });
            
            Instantiate(_businessViewPrefab, _businessParent).Setup(bs, _world, entity, 0, lvl);
            firstset = true;
        }
        
        _systems.Init ();
    }
    
    void Update () 
    {
        _systems.Run ();
    }

    void OnDestroy () 
    {
        _systems.Destroy ();
        _world.Destroy ();
    }
}
