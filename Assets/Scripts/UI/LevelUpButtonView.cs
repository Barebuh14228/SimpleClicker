using System;
using Components;
using Leopotam.Ecs;
using Settings;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelUpButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceField;
        [SerializeField] private Button _button;

        private BusinessSettings _settings;
        private int _level;
        private EcsEntity _businessEntity;
        private EcsWorld _world;
        
        private int LvlUpPrice => _settings.BasePrice * (_level + 1);
        
        private void Start()
        {
            BalanceSystem.OnBalanceChanged += OnBalanceChanged;
            LvlUpSystem.OnBusinessLvlUp += OnBusinessLvlUp;
        }

        private void OnDestroy()
        {
            BalanceSystem.OnBalanceChanged -= OnBalanceChanged;
            LvlUpSystem.OnBusinessLvlUp -= OnBusinessLvlUp;
        }

        public void Setup(EcsWorld world, EcsEntity entity, BusinessSettings settings, int level)
        {
            //todo set interactable (check balance)
            
            _world = world;
            _businessEntity = entity;
            _settings = settings;
            _level = level;
            _priceField.text = $"Цена: {LvlUpPrice} $"; //todo get text
        }
        
        private void OnBalanceChanged(int balance)
        {
            _button.interactable = LvlUpPrice <= balance;
        }
        
        private void OnBusinessLvlUp(string businessId, int level)
        {
            if (businessId != _settings.Id)
                return;

            _level++;
            _priceField.text = $"Цена: {LvlUpPrice} $"; //todo get text
        }
        
        public void OnClick()
        {
            //todo изменить способ вкида компонентов
            
            _world.NewEntity().Replace(new ChangeBalanceComponent() { Value = -LvlUpPrice });
            _businessEntity.Replace(new LvlUpComponent());
        }
    }
}