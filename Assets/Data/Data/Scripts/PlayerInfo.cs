using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Data.Scripts
{
    
    [CreateAssetMenu(menuName = "Data/Player Info", fileName = "Player Info")]
    public class PlayerInfo : ScriptableObject
    {
        public String UserName { get; set; }
        public Sprite Avatar { get; set; }
    }
}
