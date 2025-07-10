using System.Linq;
using Components;
using Leopotam.Ecs;
using Settings;
using TMPro;
using UnityEngine;

namespace UI
{
    public class RevenueView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _revenueField;

        private BusinessSettings _settings;
        private int _level;
        private EcsEntity _businessEntity;
        
        private void Start()
        {
            GameController.OnBusinessLevelUp += OnBusinessLevelUp;
            GameController.OnBusinessUpgraded += OnBusinessUpgraded;
        }

        private void OnDestroy()
        {
            GameController.OnBusinessLevelUp -= OnBusinessLevelUp;
            GameController.OnBusinessUpgraded -= OnBusinessUpgraded;
        }

        public void Setup(EcsEntity entity, BusinessSettings settings, int level)
        {
            _businessEntity = entity;
            _settings = settings;
            _level = level;
            UpdateRevenueField();
        }

        private void OnBusinessLevelUp(string businessId, int level)
        {
            if (businessId != _settings.Id)
                return;

            _level++;
            
            UpdateRevenueField();
        }

        private void OnBusinessUpgraded(string businessId, string upgradeId)
        {
            if (businessId != _settings.Id)
                return;
            
            UpdateRevenueField();
        }
        
        private void UpdateRevenueField()
        {
            //todo изменить способ расчета или способ получения информации
            
            var states = _businessEntity.Get<UpgradesComponent>().UpgradeStates;
            
            var modifier = 1 + states
                .Where(kv => kv.Value)
                .Select(kv => kv.Key)
                .Select(id => _settings.UpgradesList.First(us => us.Id == id))
                .Sum(us => us.Modifier);

            var revenue = _level * _settings.BaseRevenue * modifier;

            _revenueField.text = $"{(int) revenue} $"; //todo get text?
        }
    }
}