using Leopotam.Ecs;
using Settings;
using UI;
using UnityEngine;

//todo list

//система создания бизнесов
//сохранение/загрузка
//текста
//верстка: дизейблер и оптимизация

public class GameManager : MonoBehaviour
{
    [SerializeField] private BusinessSettingsList _settingsList;
    [SerializeField] private BusinessViewParent _businessViewParent;
    
    private EcsWorld _world;
    private EcsSystems _systems;

    private void Start()
    {
        GameController.InjectInModel(_settingsList);
        GameController.InjectInModel(_businessViewParent);
        GameController.Init();
    }

    private void Update()
    {
        GameController.Run();
    }

    private void OnDestroy()
    {
        GameController.Destroy();
    }
}
