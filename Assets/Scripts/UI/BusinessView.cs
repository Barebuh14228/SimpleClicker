using System.Linq;
using Leopotam.Ecs;
using Settings;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BusinessView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameField;
        [SerializeField] private RevenueProgressView _revenueProgressView;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private RevenueView _revenueView;
        [SerializeField] private LevelUpButtonView _levelUpButton;
        [SerializeField] private UpgradeButtonView _upgrade1Button;
        [SerializeField] private UpgradeButtonView _upgrade2Button;
        
        private BusinessSettings _settings;
        private int _level;
        
        public void Setup(BusinessSettings settings, EcsWorld world, EcsEntity entity, float revenueProgressValue, int level)
        {
            _nameField.text = settings.Id; //todo get text
            
            _settings = settings;
            _level = level;
            
            _levelView.Setup(_settings.Id, _level);
            _revenueView.Setup(entity, _settings, _level);
            _levelUpButton.Setup(world, entity, _settings, _level);
            _revenueProgressView.Setup(_settings.Id, revenueProgressValue);
            _upgrade1Button.Setup(world, entity, _settings.Id, _settings.UpgradesList.First(), false); //todo
            _upgrade2Button.Setup(world, entity, _settings.Id, _settings.UpgradesList.Last(), false); //todo
        }
    }
}