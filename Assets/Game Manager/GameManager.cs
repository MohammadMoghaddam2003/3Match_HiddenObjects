using System;
using Data.Data.Scripts;
using Scene_Loader;
using UnityEngine;

namespace Game_Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private SceneLoader sceneLoader;

        
        public static GameManager Instance;
        
        
        public PlayerInfo GetPlayerInfo { get => playerInfo; }
        public SceneLoader GetSceneLoader { get => sceneLoader; }



        protected GameManager() { }
        
        
        private void Awake() => DontDestroyOnLoad(this.gameObject);

        private void OnEnable()
        {
            if(Instance is null) Instance = this;
        }
    }
}
