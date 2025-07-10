using TMPro;
using UnityEngine;

namespace UI
{
    public class RevenueView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _revenueField;

        private string _businessId;
        
        private void Start()
        {
            GameController.OnBusinessRevenueChanged += UpdateRevenueField;
        }

        private void OnDestroy()
        {
            GameController.OnBusinessRevenueChanged -= UpdateRevenueField;
        }

        public void Setup(string businessId)
        {
            _businessId = businessId;
        }
        
        private void UpdateRevenueField(string businessId, float revenue)
        {
            if (businessId != _businessId)
                return;
            
            _revenueField.text = TextProvider.Get("money_field", $"{revenue:N0}");
        }
    }
}