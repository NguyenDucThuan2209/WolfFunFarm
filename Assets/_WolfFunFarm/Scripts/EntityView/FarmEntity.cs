using UnityEngine;

namespace WolfFunFarm
{
    public abstract class FarmEntity : MonoBehaviour
    {
        [SerializeField] protected string _entityId;
        [SerializeField] protected GameObject _productPrefab;

        public virtual string Id => _entityId;
        public virtual GameObject ProductPrefab => _productPrefab;
    }
}