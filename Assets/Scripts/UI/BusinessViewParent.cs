using UnityEngine;

namespace UI
{
    public class BusinessViewParent : MonoBehaviour
    {
        [SerializeField] private BusinessView _businessViewPrefab;

        public BusinessView InstantiateNewBusinessView()
        {
            return Instantiate(_businessViewPrefab, transform);
        }
    }
}