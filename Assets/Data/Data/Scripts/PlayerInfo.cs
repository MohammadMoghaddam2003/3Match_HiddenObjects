using System;
using UnityEngine;
using UnityEngine.UI;

namespace Data.Data.Scripts
{
    
    [CreateAssetMenu(menuName = "Data/Player Info", fileName = "Player Info")]
    public class PlayerInfo : ScriptableObject
    {
        [SerializeField] private Sprite avatar;
        [SerializeField] private String username;
        
        
        public String UserName
        {
            get => username;
            set => username = value;
        }
        public Sprite Avatar
        {
            get => avatar;
            set => avatar = value;
        }
    }
}
