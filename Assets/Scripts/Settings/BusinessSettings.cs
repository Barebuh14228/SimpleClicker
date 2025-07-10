using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "BusinessSettings", menuName = "Settings/BusinessSettings")]
    public class BusinessSettings : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private int _delay;
        [SerializeField] private int _baseRevenue;
        [SerializeField] private int _basePrice;
        [SerializeField] private UpgradeSettings _upgrade1Settings;
        [SerializeField] private UpgradeSettings _upgrade2Settings;
        
        public string Id => _id;
        public int Delay => _delay;
        public int BaseRevenue => _baseRevenue;
        public int BasePrice => _basePrice;
        public UpgradeSettings Upgrade1Settings => _upgrade1Settings;
        public UpgradeSettings Upgrade2Settings => _upgrade2Settings;
    }
}