using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private float _defaultBalance;
        [SerializeField] private List<BusinessSettings> _businessSettingsList;

        public List<BusinessSettings> BusinessSettingsList => _businessSettingsList;
        public float DefaultBalance => _defaultBalance;
    }
}