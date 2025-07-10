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

            _nameField.text = $"\"{settings.Id}\"" ; //todo get text
            _priceField.text = $"Цена: {settings.Price} $"; //todo get text
            _descriptionField.text = $"Доход: + {settings.Modifier * 100} %"; //todo get text
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
            _priceField.text = "КУПЛЕНО !"; //todo get text
        }

        public void OnClick()
        {
            GameController.SendUpgradeRequest(_businessId, _upgradeId);
        }
    }
}