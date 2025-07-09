using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "UpgradeSettings", menuName = "Settings/UpgradeSettings")]
    public class UpgradeSettings : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private int _price;
        [SerializeField] private float _modifier;
        
        public string Id => _id;
        public int Price => _price;
        public float Modifier => _modifier;
    }
}