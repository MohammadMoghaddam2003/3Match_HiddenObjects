using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour, IGameplayController
{

    #region Required Variables

    [SerializeField] private Transform gatheringPos;
    [SerializeField] private Transform[] starsPos;
    [SerializeField] private ParticleSystem collectParticleSystem;
    [SerializeField] private List<Transform> selectedItemPlace = new List<Transform>();
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


        AddItemTag(itemScript);
        MoveToBasket(itemScript);
        result = true;
    }

    
    // This method and called method inside this have a bug
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
    
    
    public void PlayCollectParticleManager()
    {
        _collectParticleManage++;
        
        if(_collectParticleManage < 3) return;

        _collectParticleManage = 0;
        PlayCollectParticle();
    }

    
    #endregion
    

    #region Utilities
    
    

    private void Start()
    {
        collectParticleSystem = Instantiate(collectParticleSystem, gatheringPos.position, Quaternion.identity);
    }
    
    
    private void AddItemTag(ItemController itemScript)
    {
        _selectedItemsCount++;
        _selectedItemScript.Add(itemScript);
    }
    
    
    private void SetMoveItem(ItemController itemController, Vector3 target) => itemController.Move(target); 
    
    
    private void MoveToBasket(ItemController itemController) => SetMoveItem(itemController,selectedItemPlace[_selectedItemsCount - 1].position);
    
    
    private void MoveToGatheringPos(ItemController itemController) => SetMoveItem(itemController,gatheringPos.position);
    

    private void ClearSelectHistory()
    {
        _selectedItemScript.Clear();
        _selectedItemsCount = 0;
        _collectableItem = null;
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
