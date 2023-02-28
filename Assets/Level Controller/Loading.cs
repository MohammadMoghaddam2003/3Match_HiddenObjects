using System;
using System.Collections;
using Game_Manager;
using Scene_Loader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Level_Controller
{
    public class Loading : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI progressText;

        private Coroutine _loadingProgress;
        private LevelController _levelController;

        
        
        
        private void OnEnable() => _loadingProgress = StartCoroutine(LoadingProgress());

        private void Start() => _levelController = GameManager.Instance.GetLevelController;


        private IEnumerator LoadingProgress()
        {
            int progressWait = Random.Range(0, 80);
            int waitTime = Random.Range(2, 6);
            float time = 0;

            while (slider.value != 1)
            {
                if (time == progressWait) yield return new WaitForSeconds(waitTime);

                time++;
                progressText.text = "Loading... " + time + "%"; 
                slider.value = time / 100;
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
            
            _levelController.StartMatch();
            StopCoroutine(_loadingProgress);
        }
    }
}
