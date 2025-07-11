using System;
using Leopotam.Ecs;
using Settings;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private TextSettings _textSettings;
    [SerializeField] private BusinessViewParent _businessViewParent;
    
    private EcsWorld _world;
    private EcsSystems _systems;

    private void Start()
    {
        TextProvider.Init(_textSettings.Texts);
        
        GameController.InjectInModel(_gameSettings);
        GameController.InjectInModel(_businessViewParent);
        GameController.InitModel();
    }

    private void Update()
    {
        GameController.RunModel();
    }

    private void OnDestroy()
    {
        GameController.DestroyModel();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            GameController.SaveModel();
        }
    }
}
