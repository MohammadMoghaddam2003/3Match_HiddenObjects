using UnityEngine;

namespace GameplayAssets.Items
{
    public class ItemChildCollider : MonoBehaviour
    {
        private Collider _collider;


        private void Awake() => _collider = GetComponent<Collider>(); 
        
        public void EnableChildCollider() => _collider.enabled = true;
        
        public void DisableChildCollider() => _collider.enabled = false;
    }
}
