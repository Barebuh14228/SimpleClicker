using System.Collections.Generic;
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
        [SerializeField] private List<UpgradeSettings> _upgradesList;
        
        public string Id => _id;
        public int Delay => _delay;
        public int BaseRevenue => _baseRevenue;
        public int BasePrice => _basePrice;
        public List<UpgradeSettings> UpgradesList => _upgradesList;
    }
}