using System.Collections;
using Controllers.Audio;
using Controllers.UI;
using Data.Events;
using Scene.Scene_Loader;
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
        private AudioController _audioController;
        
        

        private void Start()
        {
            _sceneLoader = GameManager.Instance.GetSceneLoader;
            _audioController = GameManager.Instance.GetAudioController;
        } 


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
            _audioController.StopMusic();
            _audioController.LoseSound();
            StartCoroutine(StopTime());
        }

        public void PlayerWin()
        {
            playerWin.Raise();
            _audioController.StopMusic();
            _audioController.WinSound();
           StartCoroutine( StopTime());
        }


        private void StartTime() => Time.timeScale = 1;
        

        private IEnumerator StopTime()   
        {
            yield return new WaitForSeconds(5);
            Time.timeScale = 0;
        }


    }
}
