using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "BusinessSettingsList", menuName = "Settings/BusinessSettingsList")]
    public class BusinessSettingsList : ScriptableObject
    {
        [SerializeField] private List<BusinessSettings> _settingsList;

        public List<BusinessSettings> SettingsList => _settingsList;
    }
}