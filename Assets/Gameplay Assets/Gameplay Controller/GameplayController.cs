using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour, IGameplayController
{
    [SerializeField] private Transform gatheringPos;
    [SerializeField] private Transform[] starsPos;
    [SerializeField] private List<Transform> selectedItemPlace = new List<Transform>();
    [SerializeField] private int maxStarsCount = 9; 
    
    
    private List<ItemController> _selectedItemScript = new List<ItemController>();

    private static string _collectableItem;
    private int _selectedItemsCount;
    private int _starsCount;


    public void SelectedItem(ItemController itemScript, out  bool result)
    {
        _collectableItem = _collectableItem ?? itemScript.tag;
       

        if (!itemScript.CompareTag(_collectableItem))
        {
            result = false;
            return;
        }


        AddItemTag(itemScript);
        SetMoveItem(itemScript);
        result = true;
    }


    public void RemoveItem(ItemController itemScript)
    {
       ClearSelectHistory();
    }


    public void AddStar()
    {
        _starsCount++;
        
        // Get star from pooling object and get a pos to this parameter

        WinController();
    }



    private void AddItemTag(ItemController itemScript)
    {
        _selectedItemsCount++;
        _selectedItemScript.Add(itemScript);
    }

    
    private void SetMoveItem(ItemController itemController)
    {
        itemController.Move(selectedItemPlace[_selectedItemsCount - 1].position); 
    }
    
    
    public IEnumerator SelectedItemsController()
    {
        if(_selectedItemsCount == 3)
        {
            if (ValidationChecker())
            {
                yield return new WaitForSeconds(.25f);
                for (int i = 0; i < _selectedItemScript.Count; i++)
                {
                    _selectedItemScript[i].SetCollectedAll = true;
                    _selectedItemScript[i].Move(gatheringPos.position);
                }

                ClearSelectHistory();
            }
        }
        
    }


    private void ClearSelectHistory()
    {
        _selectedItemScript.Clear();
        _selectedItemsCount = 0;
        _collectableItem = null;
    }
    

    private bool ValidationChecker() => ((_selectedItemScript[0].CompareTag(_selectedItemScript[1].tag)) && _selectedItemScript[1].CompareTag(_selectedItemScript[2].tag));
    
    
    
    private void WinController()
    {
        if (WinChecker())
        {
            // Call the level controller "Win" method
        }
    }

    private bool WinChecker() => _starsCount == maxStarsCount;
}
