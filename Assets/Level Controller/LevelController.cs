using System;
using Game_Manager;
using Scene_Loader;
using UnityEngine;

namespace Level_Controller
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
