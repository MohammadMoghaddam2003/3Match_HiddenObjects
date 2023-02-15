using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour, IGameplayController
{

    #region Required Variables

    [SerializeField] private Transform gatheringPos;
    [SerializeField] private Transform[] starsPos;
    [SerializeField] private ParticleSystem collectParticleSystem;
    [SerializeField] private Baskets[] baskets;
    [SerializeField] private int maxStarsCount = 9;

    #endregion
  

    #region Private Fields

    private readonly List<ItemController> _selectedItemScript = new List<ItemController>();
    private static string _collectableItem;
    private int _selectedItemsCount;
    private int _starsCount;
    private int _collectParticleManage;

    #endregion




    #region Public Methods

    public void SelectedItem(ItemController itemScript, out  bool result)
    {
        _collectableItem = _collectableItem ?? itemScript.tag;
       

        if (!itemScript.CompareTag(_collectableItem))
        {
            result = false;
            return;
        }


        AddItem(itemScript);
        MoveToBasket(itemScript);
        SetItemBasket(itemScript,baskets[_selectedItemsCount - 1].GetParent);

        result = true;
    }

    
    public void RemoveItem(ItemController itemScript, Transform basket)
    {
        if (_selectedItemsCount > 1)
        {
            _selectedItemsCount--;
            BasketManager(itemScript,0 ,true);
            _selectedItemScript.Remove(itemScript);
            return;
        }
        
        
        ClearSelectHistory();
    }
    
    
    public void AddStar()
    {
        _starsCount++;
        
        // Get star from pooling object and get a pos to this parameter

        WinController();
    }

    
    public void PlayCollectParticleManager()
    {
        _collectParticleManage++;
        
        if(_collectParticleManage < 3) return;

        _collectParticleManage = 0;
        PlayCollectParticle();
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
                    MoveToGatheringPos(_selectedItemScript[i]);
                }

                ClearSelectHistory();
            }
        }
        
    }
    
    
    
    #endregion
    

    #region Utilities
    
    

    private void Start()
    {
        collectParticleSystem = Instantiate(collectParticleSystem, gatheringPos.position, Quaternion.identity);
    }
    
    
    private void AddItem(ItemController itemScript)
    {
        _selectedItemsCount++;
        _selectedItemScript.Add(itemScript);
    }
    
    
    private void SetMoveItem(ItemController itemController, Vector3 target) => itemController.Move(target);


    private void MoveToBasket(ItemController itemController)
    {
        for (int i = 0; i < baskets.Length; i++)
        {
            if (!baskets[i].GetUsed)
            {
                SetMoveItem(itemController, baskets[i].GetPos);
                BasketManager(itemController, i);
                break;
            }
        }
    }


    private void MoveToGatheringPos(ItemController itemController) => SetMoveItem(itemController,gatheringPos.position);


    private void BasketManager(ItemController itemController, int indexBasket = 0, bool isRemove = false)
    {
        if (isRemove)
        {
            for (int i = 0; i < baskets.Length; i++)
            {
                if(itemController.Basket == baskets[i].GetParent) SetBasketDontUsing(baskets[i]);
            }

            return;
        }
        
        SetBasketUsing(baskets[indexBasket]);
    }
    

    private void SetItemBasket(ItemController itemController, Transform basket) => itemController.Basket = basket;
    
    
    private void SetBasketDontUsing(Baskets basket) => basket.SetUsedFalse();


    private void SetBasketUsing(Baskets basket) => basket.SetUsedTrue();
    


    private void ClearSelectHistory()
    {
        _selectedItemScript.Clear();
        _selectedItemsCount = 0;
        _collectableItem = null;
        
        ClearBasketUsing();
    }


    private void ClearBasketUsing()
    {
        for (int i = 0; i < baskets.Length; i++)
        {
            baskets[i].SetUsedFalse();
        }
    }


    private void WinController()
    {
        if (WinChecker())
        {
            // Call the level controller "Win" method
        }
    }
    

    private void PlayCollectParticle() => collectParticleSystem.Play();
    
    
    private bool WinChecker() => _starsCount == maxStarsCount;
    
    
    private bool ValidationChecker() => ((_selectedItemScript[0].CompareTag(_selectedItemScript[1].tag)) && _selectedItemScript[1].CompareTag(_selectedItemScript[2].tag));
    
    
    

    #endregion
    
}
