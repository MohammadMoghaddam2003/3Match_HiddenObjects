using UnityEngine;
using Game_Manager;
using GameplayAssets.Audio;
using GameplayAssets.Level;

namespace GameplayAssets.Menu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject signinMenu;


        private LevelController _levelController;
        private AudioController _audioController;



        private void Start()
        {
            _levelController = GameManager.Instance.GetLevelController;
            _audioController = GameManager.Instance.GetAudioController;
        } 
        
        public void GoStartMenu()
        {
            PlayClickSound();
            startMenu.SetActive(true);
            signinMenu.SetActive(false);
        }
        
        public void GoSigninMenu()
        {
            PlayClickSound();
            signinMenu.SetActive(true);
            startMenu.SetActive(false);
        }

        public void SigninCompleted()
        {
            PlayClickSound();
            _levelController.LoadLevel();
        } 
        
        public void PlayClickSound() => _audioController.Click();
        
    }
}
