using Controllers.Audio;
using Controllers.Data;
using Controllers.Level;
using Data.Data;
using Scene.Scene_Loader;
using UnityEngine;

namespace Game_Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private LevelController levelController;
        [SerializeField] private DataController dataController;
        [SerializeField] private AudioController audioController;

        
        public static GameManager Instance;
        
        
        public SceneLoader GetSceneLoader { get => sceneLoader; }
        public LevelController GetLevelController { get => levelController; }
        public PlayerInfo GetPlayerInfo { get => dataController.GetPlayerInfo; }
        public PlayerInfo GetOpponentInfo { get => dataController.GetOpponentInfo; }
        public GameplayData GetGameplayData { get => dataController.GetGameplayData; }
        public AudioController GetAudioController { get => audioController; }



        protected GameManager() { }
        
        
        private void OnEnable()
        {
            if(Instance is null) Instance = this;
        }
    }
}
