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


        public void GoToStart()
        {
            StartTime();
            _sceneLoader.LoadMenuScene();  
        } 
        
        public void LoadLevel()
        {
            StartTime();
            _sceneLoader.GoToLoading();
        } 

        public void StartMatch()
        {
            StartTime();
            _sceneLoader.LoadGameplayScene();
        } 

        public void PlayerLose()
        {
            playerLose.Raise();
            StopTime();
        }

        public void PlayerWin()
        {
            playerWin.Raise();
            StopTime();
        }


        private void StartTime() => Time.timeScale = 1;
        private void StopTime() => Time.timeScale = 0;


    }
}
