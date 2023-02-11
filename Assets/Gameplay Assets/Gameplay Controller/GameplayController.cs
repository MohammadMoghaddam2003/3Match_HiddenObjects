using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour, IGameplayController
{
    [SerializeField] private Transform gatheringPos;
    [SerializeField] private Transform[] starsPos;
    [SerializeField] private List<SelectedItemPlace> selectedItemPlace = new List<SelectedItemPlace>();
    [SerializeField] private int maxStarsCount = 9; 
    
    
    private List<string> _selectedItems = new List<string>();
    private int _selectedItemsCount;
    private int _starsCount;


    public void SelectedItem(Transform item)
    {
        AddItemTag(item.tag);
        SetMoveItem(item.GetComponent<ItemController>());
        SelectedItemsController();
    }


    public void RemoveItem(string itemTag)
    {
        _selectedItemsCount--;
        _selectedItems.Remove(itemTag);
    }


    public void AddStar()
    {
        _starsCount++;
        
        // Get star from pooling object and get a pos to this parameter

        WinController();
    }



    private void AddItemTag(string itemTag)
    {
        _selectedItemsCount++;
        _selectedItems.Add(itemTag);
    }

    
    private void SetMoveItem(ItemController itemController)
    {
        for (int i = 0; i < selectedItemPlace.Count; i++)
        {
            if (!selectedItemPlace[i].Used)
            {
                itemController.Move(selectedItemPlace[i].GetPos);
                return;
            }
        }
    }
    
    
    private void SelectedItemsController()
    {
        if(_selectedItemsCount == 3)
        {
            if (ValidationChecker())
            {
                // Start the move enumerator of items and get the "gatheringPos" to they parameter

                for (int i = 0; i < _selectedItems.Count; i++)
                {
                }
            }
            else
            {
                // Call the level controller "Lose" method
            }
        }
        
    }
    

    private bool ValidationChecker() => ((_selectedItems[0] == _selectedItems[1]) && _selectedItems[1] == _selectedItems[2]);
    
    
    
    private void WinController()
    {
        if (WinChecker())
        {
            // Call the level controller "Win" method
        }
    }

    private bool WinChecker() => _starsCount == maxStarsCount;
}
