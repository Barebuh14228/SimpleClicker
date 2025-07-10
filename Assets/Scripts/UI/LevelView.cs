using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelField;

        private string _businessId;
        
        private void Start()
        {
            GameController.OnBusinessLevelUp += OnBusinessLevelUp;
        }

        private void OnDestroy()
        {
            GameController.OnBusinessLevelUp -= OnBusinessLevelUp;
        }
        
        public void Setup(string businessId, int level)
        {
            _businessId = businessId;
            _levelField.text = $"{level}";
        }

        private void OnBusinessLevelUp(string businessId, int level)
        {
            if (businessId != _businessId)
                return;

            _levelField.text = $"{level}";
        }
    }
}