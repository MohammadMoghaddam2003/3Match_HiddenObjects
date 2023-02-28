using UnityEngine;

namespace Menu.Scripts
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject signinMenu;
        [SerializeField] private GameObject loadingPage;

        
        

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
        
        
        
        public void GoLoading()
        {
            signinMenu.SetActive(false);
            loadingPage.SetActive(true);
        }
    }
}
