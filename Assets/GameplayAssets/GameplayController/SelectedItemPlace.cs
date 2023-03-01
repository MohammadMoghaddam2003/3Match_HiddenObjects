using System;
using UnityEngine;

namespace GameplayAssets.GameplayController
{
    [Serializable]
    public class Baskets
    {
        [SerializeField] private Transform pos;

    
        private bool _used;

        public Vector3 GetPos { get => pos.position; }
        
        public Transform GetParent { get => pos.parent; }

        public bool GetUsed { get => _used; }

        public void SetUsedTrue() => _used = true; 

        public void SetUsedFalse() => _used = false;
        
    }
}
