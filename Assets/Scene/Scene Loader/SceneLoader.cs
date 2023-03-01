using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Loader
{
   public class SceneLoader : MonoBehaviour
   {
      private void Awake() => DontDestroyOnLoad(this.gameObject);
      


      
      public void LoadMenuScene() => SceneManager.LoadSceneAsync(0);
      
   
      public void GoToLoading() => SceneManager.LoadSceneAsync(1);
      
   
      public void LoadGameplayScene() => SceneManager.LoadSceneAsync(2);
      
   }
}
