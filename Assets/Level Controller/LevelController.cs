using System;
using UnityEngine;

namespace Level_Controller
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameObject loadingPage;


        private void Awake() => DontDestroyOnLoad(this.gameObject);


        public void LoadLevel() => loadingPage.SetActive(true);
        
    }
}
