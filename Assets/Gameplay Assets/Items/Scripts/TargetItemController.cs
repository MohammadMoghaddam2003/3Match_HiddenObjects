using System;
using UnityEngine;

namespace Gameplay_Assets.Items.Scripts
{
    
    [Serializable]
    public class TargetItemController
    {
        [SerializeField] private string tag;
        [SerializeField] private bool used;
        
        
        
        
        public string Tag
        {
            get => tag;
            set => tag = value;
        }
        
        
        public bool Used
        {
            get => used;
            set => used = value;
        }
    }
}
