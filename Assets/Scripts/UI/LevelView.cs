using Systems;
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
            LvlUpSystem.OnBusinessLvlUp += OnBusinessLvlUp;
        }

        private void OnDestroy()
        {
            LvlUpSystem.OnBusinessLvlUp -= OnBusinessLvlUp;
        }
        
        public void Setup(string businessId, int level)
        {
            _businessId = businessId;
            _levelField.text = $"{level}";
        }

        private void OnBusinessLvlUp(string businessId, int level)
        {
            if (businessId != _businessId)
                return;

            _levelField.text = $"{level}";
        }
    }
}