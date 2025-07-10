using TMPro;
using UnityEngine;

namespace UI
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _balanceField;
        
        private void Start()
        {
            GameController.OnBalanceChanged += OnBalanceChanged;
        }

        private void OnDestroy()
        {
            GameController.OnBalanceChanged -= OnBalanceChanged;
        }
        
        private void OnBalanceChanged(float balance)
        {
            _balanceField.text = $"{balance:N0} $";
        }
    }
}