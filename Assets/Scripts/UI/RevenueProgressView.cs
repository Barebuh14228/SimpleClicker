using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RevenueProgressView : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;

        private string _businessId;

        private void Start()
        {
            GameController.OnRevenueProgressChanged += OnRevenueProgressChanged;
        }

        private void OnDestroy()
        {
            GameController.OnRevenueProgressChanged -= OnRevenueProgressChanged;
        }

        public void Setup(string businessId, float value)
        {
            _businessId = businessId;
            _progressBar.value = value;
        }
        
        private void OnRevenueProgressChanged(string businessId, float progress)
        {
            if (businessId != _businessId)
                return;
            
            _progressBar.value = progress;
        }
    }
}