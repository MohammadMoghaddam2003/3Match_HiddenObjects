using System;
using Data.Data.Scripts;
using UnityEngine;

namespace Game_Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;

        
        public static GameManager Instance;
        
        
        public PlayerInfo GetPlayerInfo { get => playerInfo; }



        protected GameManager() { }
        
        
        private void Awake() => DontDestroyOnLoad(this.gameObject);

        private void OnEnable()
        {
            if(Instance is null) Instance = this;
        }
    }
}
