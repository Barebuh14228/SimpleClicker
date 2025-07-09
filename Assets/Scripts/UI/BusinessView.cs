using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;
using Systems;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BusinessView : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TMP_Text _levelField;
        [SerializeField] private TMP_Text _revenueField;
        [SerializeField] private TMP_Text _lvlUpPriceField;
        [SerializeField] private TMP_Text _upgrade1PriceField;
        [SerializeField] private TMP_Text _upgrade1DescriptionField;
        [SerializeField] private TMP_Text _upgrade2PriceField;
        [SerializeField] private TMP_Text _upgrade2DescriptionField;
        [SerializeField] private Button _lvlUpButton;
        [SerializeField] private Button _upgrade1Button;
        [SerializeField] private Button _upgrade2Button;

        private BusinessSettings _settings;
        
        private EcsWorld _world;
        private EcsEntity _businessEntity;
        private int _level;

        private int LvlUpPrice => _settings.BasePrice * (_level + 1);
        
        //todo revenue field
        
        public void Setup(BusinessSettings settings, EcsWorld world, EcsEntity entity, float revenueProgressValue, int level)
        {
            _settings = settings;
            _businessEntity = entity;
            _progressBar.value = revenueProgressValue;
            _level = level;
            _levelField.text = _level.ToString();
            _world = world;
            _lvlUpPriceField.text = LvlUpPrice.ToString();
            _upgrade1PriceField.text = settings.UpgradesList.First().Price.ToString();
            _upgrade2PriceField.text = settings.UpgradesList.Last().Price.ToString();
            _upgrade1DescriptionField.text = settings.UpgradesList.First().Modifier.ToString();
            _upgrade2DescriptionField.text = settings.UpgradesList.Last().Modifier.ToString();
        }
        
        private void Start()
        {
            RevenueProgressSystem.OnRevenueProgressChanged += OnRevenueProgressChanged;
            BalanceSystem.OnBalanceChanged += OnBalanceChanged;
            LvlUpSystem.OnBusinessLvlUp += OnBusinessLvlUp;
            UpgradePurchaseSystem.OnBusinessUpgraded += OnBusinessUpgraded;
        }

        private void OnDestroy()
        {
            RevenueProgressSystem.OnRevenueProgressChanged -= OnRevenueProgressChanged;
            BalanceSystem.OnBalanceChanged -= OnBalanceChanged;
            LvlUpSystem.OnBusinessLvlUp -= OnBusinessLvlUp;
            UpgradePurchaseSystem.OnBusinessUpgraded -= OnBusinessUpgraded;
        }

        private void OnRevenueProgressChanged(string id, float progress)
        {
            if (id != _settings.Id)
                return;
            
            _progressBar.value = progress;
        }
        
        private void OnBalanceChanged(int balance)
        {
            var lvlUpPrice = LvlUpPrice;
            var upgrade1Price = _settings.UpgradesList.First().Price;
            var upgrade2Price = _settings.UpgradesList.Last().Price;

            var states = _businessEntity.Get<UpgradesComponent>().UpgradeStates;
            
            _lvlUpButton.interactable = lvlUpPrice <= balance;
            _upgrade1Button.interactable = upgrade1Price <= balance && !states.First().Value;
            _upgrade2Button.interactable = upgrade2Price <= balance && !states.Last().Value;
        }
        
        private void OnBusinessLvlUp(string id, int level)
        {
            if (id != _settings.Id)
                return;

            _level++;
            _levelField.text = _level.ToString();
            _lvlUpPriceField.text = LvlUpPrice.ToString();
        }
        
        private void OnBusinessUpgraded(string businessId, string upgradeId)
        {
            if (businessId != _settings.Id)
                return;

            if (_settings.UpgradesList.First().Id == upgradeId) _upgrade1Button.interactable = false;
            if (_settings.UpgradesList.Last().Id == upgradeId) _upgrade2Button.interactable = false;
        }

        public void OnLlvUpButtonClick()
        {
            _world.NewEntity().Replace(new ChangeBalanceComponent() { Value = -LvlUpPrice });
            _businessEntity.Replace(new LvlUpComponent());
        }
        
        public void OnUpgrade1ButtonClick()
        {
            var upgrade = _settings.UpgradesList.First();

            _world.NewEntity().Replace(new ChangeBalanceComponent() { Value = -upgrade.Price });
            _businessEntity.Replace(new UpgradePurchaseComponent() { Id = upgrade.Id });
        }
        
        public void OnUpgrade2ButtonClick()
        {
            var upgrade = _settings.UpgradesList.Last();

            _world.NewEntity().Replace(new ChangeBalanceComponent() { Value = -upgrade.Price });
            _businessEntity.Replace(new UpgradePurchaseComponent() { Id = upgrade.Id });
        }
    }
}