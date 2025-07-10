using System.Linq;
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
        
        public void Setup(BusinessSettings settings, float revenueProgressValue, int level)
        {
            _nameField.text = settings.Id; //todo get text
            
            _levelView.Setup(settings.Id, level);
            _revenueView.Setup(settings.Id);
            _levelUpButton.Setup(settings.Id);
            _revenueProgressView.Setup(settings.Id, revenueProgressValue);
            
            _upgrade1Button.Setup(settings.Id, settings.UpgradesList.First()); //todo
            _upgrade2Button.Setup(settings.Id, settings.UpgradesList.Last()); //todo
        }
    }
}