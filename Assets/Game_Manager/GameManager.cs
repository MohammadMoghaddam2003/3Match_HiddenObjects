using Controllers.Level;
using Scene_Loader;
using UnityEngine;

namespace Game_Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private LevelController levelController;

        
        public static GameManager Instance;
        
        
        public SceneLoader GetSceneLoader { get => sceneLoader; }
        public LevelController GetLevelController { get => levelController; }



        protected GameManager() { }
        
        
        private void Awake() => DontDestroyOnLoad(this.gameObject);

        private void OnEnable()
        {
            if(Instance is null) Instance = this;
        }
    }
}
