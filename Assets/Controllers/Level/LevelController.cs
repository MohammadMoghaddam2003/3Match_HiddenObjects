using Scene_Loader;
using Game_Manager;
using UnityEngine;

namespace Controllers.Level
{
    public class LevelController : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        
        
        
        private void Awake() => DontDestroyOnLoad(this.gameObject);

        private void Start() => _sceneLoader = GameManager.Instance.GetSceneLoader;

        
        
        public void LoadLevel() => _sceneLoader.GoToLoading();

        public void StartMatch() => _sceneLoader.LoadGameplayScene();

    }
}
