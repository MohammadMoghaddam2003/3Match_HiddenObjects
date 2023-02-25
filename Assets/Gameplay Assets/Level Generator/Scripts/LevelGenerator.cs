using System.Collections;
using System.Collections.Generic;
using Data.Data.Scripts;
using Gameplay_Assets.Items.Scripts;
using UnityEngine;

namespace Gameplay_Assets.Level_Generator.Scripts
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();
        [SerializeField] private GameplayData gameplayData;
        [SerializeField] private Transform[] generatePos = new Transform[3];

        
        private List<GameObject> _targetItems = new List<GameObject>();
        private List<GameObject> _otherItems = new List<GameObject>();
        private TargetItemController[] _targetItemController;
        
        
        void Start()
        {
            ChooseItems(gameplayData.GetTargetItemCount,_targetItems);
            SetTargetsToData();
            StartGeneration(_targetItems);
            
            ChooseItems(gameplayData.GetOtherItemCount,_otherItems);
            StartGeneration(_otherItems);
        }
        
        
        private void ChooseItems(int length, List<GameObject> list)
        {
            for (int i = 0; i < length; i++)
            {
                int randomIndex = Random.Range(0, (itemPrefabs.Count));
                list.Add(itemPrefabs[randomIndex]);
                itemPrefabs.Remove(itemPrefabs[randomIndex]);
            }
        }

        
        private void SetTargetsToData()
        {
            _targetItemController = new TargetItemController[gameplayData.GetTargetItemCount];
            
            for (int i = 0; i < _targetItems.Count; i++)
            {
                _targetItemController[i] = new TargetItemController();
                _targetItemController[i].Tag = _targetItems[i].tag;
            }

            gameplayData.TargetItemControllers = _targetItemController;
        }


        private void StartGeneration(List<GameObject> list)
        {
            StopCoroutine(GenerateItem(list));
            StartCoroutine(GenerateItem(list));
        }
        

        private IEnumerator GenerateItem(List<GameObject> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    yield return new WaitForSeconds(.02f);
                    Instantiate(list[i], generatePos[j].position, Quaternion.identity);
                }
            }
        }
    }
}
