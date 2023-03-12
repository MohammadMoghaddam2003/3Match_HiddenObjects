using GameplayAssets.Audio;
using GameplayAssets.Level;
using GameData.Data;
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


        private void Awake() => DontDestroyOnLoad(this.gameObject);

        public SceneLoader GetSceneLoader { get => sceneLoader; }
        public LevelController GetLevelController { get => levelController; }
        public PlayerInfo GetPlayerInfo { get => dataController.GetPlayerInfo; }
        public PlayerInfo GetOpponentInfo { get => dataController.GetOpponentInfo; }
        public AudioController GetAudioController { get => audioController; }



        private GameManager() { }
        
        private void OnEnable()
        {
            if(Instance is null) Instance = this;
            else Destroy(gameObject);
        }
    }
}
