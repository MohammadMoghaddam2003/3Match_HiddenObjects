using Controllers.UI;
using Data.Events;
using Scene_Loader;
using Game_Manager;
using UnityEngine;

namespace Controllers.Level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private EventSO playerLose;
        [SerializeField] private EventSO playerWin;
        
        private SceneLoader _sceneLoader;
        private UIController _uiController;
        
        
        
        private void Awake() => DontDestroyOnLoad(this.gameObject);

        private void Start() => _sceneLoader = GameManager.Instance.GetSceneLoader;

        
        
        public void LoadLevel() => _sceneLoader.GoToLoading();

        public void StartMatch() => _sceneLoader.LoadGameplayScene();

        public void PlayerLose()
        {
            playerLose.Raise();
            Time.timeScale = 0;
        }

        public void PlayerWin()
        {
            playerWin.Raise();
            Time.timeScale = 0;
        }

    }
}
