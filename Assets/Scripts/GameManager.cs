using Leopotam.Ecs;
using Settings;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private BusinessViewParent _businessViewParent;
    
    private EcsWorld _world;
    private EcsSystems _systems;

    private void Start()
    {
        GameController.InjectInModel(_gameSettings);
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
