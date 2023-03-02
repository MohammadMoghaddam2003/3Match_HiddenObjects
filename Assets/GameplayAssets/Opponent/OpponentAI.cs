using System;
using System.Collections;
using Controllers.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameplayAssets.Opponent
{
    public class OpponentAI : MonoBehaviour
    {
        [SerializeField] private UIController uIController;
        [SerializeField] private int minSecond = 3;
        [SerializeField] private int maxSecond = 15;


        private void Start()
        {
            StartCoroutine(AISimulate());
        }


        private IEnumerator AISimulate()
        {
            int time = Random.Range(minSecond, maxSecond + 1);

            yield return new WaitForSeconds(time);
            uIController.ChangeOpponentStarSprite();
            StartCoroutine(AISimulate());
        }
        
    }
}
