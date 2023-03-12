using UnityEngine;

namespace GameplayAssets.Items
{

    [RequireComponent(typeof(Collider))]
    public class ItemColliderManagement : MonoBehaviour
    {
        private Collider _collider;

        private void Awake() => _collider = GetComponent<Collider>();
        
        public void EnableCollider()
        {
            ItemMovement.FinishedReturn -= EnableCollider;
            _collider.enabled = true;  
        } 
        
        public void DisableCollider() => _collider.enabled = false;
    }
}
