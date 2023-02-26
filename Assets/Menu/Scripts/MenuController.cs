using UnityEngine;

namespace Menu.Scripts
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject signinMenu;
        [SerializeField] private GameObject settingMenu;

        
        

        public void GoStartMenu()
        {
            startMenu.SetActive(true);
            signinMenu.SetActive(false);
            settingMenu.SetActive(false);
        }
        
        
        public void GoSigninMenu()
        {
            signinMenu.SetActive(true);
            startMenu.SetActive(false);
            settingMenu.SetActive(false);
        }
        
        
        public void GoToSetting()
        {
            settingMenu.SetActive(true);
            signinMenu.SetActive(false);
            startMenu.SetActive(false);
        }
    }
}
