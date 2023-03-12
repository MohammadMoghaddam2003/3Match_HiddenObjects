using System.Collections;
using Game_Manager;
using GameplayAssets.Opponent;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameplayAssets.Level
{
    public class Loading : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI progressText;
        [SerializeField] private OpponentFinder opponentFinder;

        private Coroutine _loadingProgress;
        private LevelController _levelController;

        
        
        
        private void OnEnable() => _loadingProgress = StartCoroutine(LoadingProgress());

        private void Start() => _levelController = GameManager.Instance.GetLevelController;
        
        private IEnumerator LoadingProgress()
        {
            int progressWait = Random.Range(20, 80);
            int waitTime = Random.Range(2, 4);
            float time = 0;

            while (slider.value != 1)
            {
                if (time == progressWait) yield return new WaitForSeconds(waitTime);
                if (time == progressWait) opponentFinder.ShowOpponent();

                time++;
                progressText.text = "Loading... " + time + "%"; 
                slider.value = time / 100;
                yield return new WaitForSeconds(.02f);
            }
            
            StopCoroutine(_loadingProgress);
            _levelController.StartMatch();
        }
    }
}
