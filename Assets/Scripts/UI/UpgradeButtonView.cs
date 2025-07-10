using Settings;
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
        private string _upgradeId;
        private float _price;
        private bool _isPurchased;

        public void Start()
        {
            GameController.OnBalanceChanged += OnBalanceChanged;
            GameController.OnBusinessUpgraded += OnBusinessUpgraded;
        }

        private void OnDestroy()
        {
            GameController.OnBalanceChanged -= OnBalanceChanged;
            GameController.OnBusinessUpgraded -= OnBusinessUpgraded;
        }

        public void Setup(string businessId, UpgradeSettings settings)
        {
            _businessId = businessId;
            _upgradeId = settings.Id;
            _price = settings.Price;

            var upgradeTitle = TextProvider.Get(_upgradeId);
            
            _nameField.text = TextProvider.Get("quote_text", upgradeTitle);
            _priceField.text = TextProvider.Get("price_field", settings.Price);
            _descriptionField.text = TextProvider.Get("revenue_mod_field", settings.Modifier * 100);
        }
        
        private void OnBalanceChanged(float balance)
        {
            _button.interactable = _price <= balance && !_isPurchased;
        }
        
        private void OnBusinessUpgraded(string businessId, string upgradeId)
        {
            if (businessId != _businessId)
                return;
            
            if (upgradeId != _upgradeId)
                return;

            _isPurchased = true;
            _button.interactable = false;
            _priceField.text = TextProvider.Get("purchased");
        }

        public void OnClick()
        {
            GameController.SendUpgradeRequest(_businessId, _upgradeId);
        }
    }
}