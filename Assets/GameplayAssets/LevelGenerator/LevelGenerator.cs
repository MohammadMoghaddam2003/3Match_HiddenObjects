using System.Collections;
using System.Collections.Generic;
using GameData.Data;
using GameplayAssets.Items;
using UnityEngine;

namespace GameplayAssets.LevelGenerator
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> itemPrefabs = new List<GameObject>();
        [SerializeField] private GameplayData gameplayData;
        [SerializeField] private Transform[] generatePos = new Transform[3];

        [SerializeField] private Gameplay.GameplayController gameplayController;

        
        private List<GameObject> _items = new List<GameObject>();
        private ItemController _itemController;
        private string[] _targetItemController;




        private void Start()
        {
            ChooseItems(gameplayData.GetItemCount,_items);
            StartGeneration(_items);
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
                   _itemController = Instantiate(list[i], generatePos[j].position, Quaternion.identity).GetComponent<ItemController>();
                   _itemController.SetGameplayController = gameplayController;
                }
            }
        }
    }
}
