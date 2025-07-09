using Systems;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balanceField;
        
        private void Start()
        {
            BalanceSystem.OnBalanceChanged += OnBalanceChanged;
        }

        private void OnDestroy()
        {
            BalanceSystem.OnBalanceChanged -= OnBalanceChanged;
        }

        public void Setup(int balance)
        {
            _balanceField.text = balance.ToString();
        }
        
        private void OnBalanceChanged(int balance)
        {
            _balanceField.text = balance.ToString();
        }
    }
}