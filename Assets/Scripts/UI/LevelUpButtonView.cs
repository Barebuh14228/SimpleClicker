using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelUpButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _priceField;
        [SerializeField] private Button _button;

        private string _businessId;
        private float _price;
        
        private void Start()
        {
            GameController.OnBalanceChanged += OnBalanceChanged;
            GameController.OnLevelUpPriceChanged += OnLevelUpPriceChanged;
        }

        private void OnDestroy()
        {
            GameController.OnBalanceChanged -= OnBalanceChanged;
            GameController.OnLevelUpPriceChanged -= OnLevelUpPriceChanged;
        }

        public void Setup(string businessId)
        {
            _businessId = businessId;
        }
        
        private void OnBalanceChanged(float balance)
        {
            _button.interactable = _price <= balance;
        }
        
        private void OnLevelUpPriceChanged(string businessId, float price)
        {
            if (businessId != _businessId)
                return;
            
            _price = price;
            
            _priceField.text = TextProvider.Get("price_field", $"{_price:N0}");
        }
        
        public void OnClick()
        {
            GameController.SendLevelUpRequest(_businessId);
        }
    }
}