using System;
using Game_Manager;
using Level_Controller;
using UnityEngine;

namespace Menu.Scripts
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject signinMenu;


        private LevelController _levelController;


        private void Awake() => DontDestroyOnLoad(this.gameObject);

        private void Start() => _levelController = GameManager.Instance.GetLevelController;


        
        public void GoStartMenu()
        {
            startMenu.SetActive(true);
            signinMenu.SetActive(false);
        }
        
        
        public void GoSigninMenu()
        {
            signinMenu.SetActive(true);
            startMenu.SetActive(false);
        }
        
        
        
        public void SigninCompleted()
        {
            signinMenu.SetActive(false);
            _levelController.LoadLevel();
        }
    }
}
