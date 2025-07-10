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
        
        public void Setup(string businessId, int level, float revenueProgress, UpgradeSettings upgrade1Settings, UpgradeSettings upgrade2Settings)
        {
            _nameField.text = TextProvider.Get(businessId);
            
            _levelView.Setup(businessId, level);
            _revenueView.Setup(businessId);
            _levelUpButton.Setup(businessId);
            _revenueProgressView.Setup(businessId, revenueProgress);
            
            _upgrade1Button.Setup(businessId, upgrade1Settings);
            _upgrade2Button.Setup(businessId, upgrade2Settings);
        }
    }
}