using Components;
using Leopotam.Ecs;
using Settings;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private TMP_Text _priceField;
        [SerializeField] private TMP_Text _descriptionField; 
        [SerializeField] private Button _button;

        private string _businessId;
        private UpgradeSettings _settings;
        private bool _isPurchased;
        private EcsEntity _businessEntity;
        private EcsWorld _world;

        public void Start()
        {
            BalanceSystem.OnBalanceChanged += OnBalanceChanged;
            UpgradePurchaseSystem.OnBusinessUpgraded += OnBusinessUpgraded;
        }

        private void OnDestroy()
        {
            BalanceSystem.OnBalanceChanged -= OnBalanceChanged;
            UpgradePurchaseSystem.OnBusinessUpgraded -= OnBusinessUpgraded;
        }

        public void Setup(EcsWorld world, EcsEntity entity, string businessId, UpgradeSettings settings, bool isPurchased)
        {
            //todo set interactable (check balance)
            
            _world = world;
            _businessEntity = entity;
            _businessId = businessId;
            _settings = settings;
            _isPurchased = isPurchased;

            _nameField.text = $"\"{_settings.Id}\"" ; //todo get text
            _priceField.text = $"Цена: {_settings.Price} $"; //todo get text
            _descriptionField.text = $"Доход: + {_settings.Modifier * 100} %"; //todo get text
        }
        
        private void OnBalanceChanged(int balance)
        {
            _button.interactable = _settings.Price <= balance && !_isPurchased;
        }
        
        private void OnBusinessUpgraded(string businessId, string upgradeId)
        {
            if (businessId != _businessId)
                return;
            
            if (upgradeId != _settings.Id)
                return;

            _isPurchased = true;
            _button.interactable = false;
        }

        public void OnClick()
        {
            //todo изменить способ вкида компонентов
            
            _world.NewEntity().Replace(new ChangeBalanceComponent() { Value = -_settings.Price });
            _businessEntity.Replace(new UpgradePurchaseComponent() { Id = _settings.Id });
        }
    }
}