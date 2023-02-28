using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Loader
{
   public class SceneLoader : MonoBehaviour
   {
      private void Awake()
      {
         DontDestroyOnLoad(this.gameObject);
      }


      public void LoadMenuScene()
      {
         SceneManager.LoadScene(0);
      }
   
   
      public void LoadGameplayScene()
      {
         SceneManager.LoadScene(1);
      }
   }
}
