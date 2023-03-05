using System.Collections;
using System.Text;
using Controllers.UI;
using Data.Data;
using Data.Events;
using GameplayAssets.Star;
using UnityEngine;

namespace Controllers.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private Transform gatheringPos;
        [SerializeField] private Transform[] starsPos;
        [SerializeField] private ParticleSystem collectParticleSystem;
        [SerializeField] private GameplayData gameplayData;
        [SerializeField] private EventSO completeItemEvent;
        [SerializeField] private GameObject star;
        [SerializeField] private UIController uIController;
        [SerializeField] private Transform canvas;
        [SerializeField] private int maxStarsCount = 9;

        
        private static StringBuilder _collectableItem;


        private StarMovement _starMovement;
        private int _selectedItemsCount;
        private int _starsCount;
        private int _collectParticleManage;
        

        
        
        
        private void Awake() => ResetGameplayData();


        public void SelectedItem()
        {
             _collectableItem ??= gameplayData.SelectedItemTag;
            
            if (gameplayData.SelectedItemTag.ToString() != _collectableItem.ToString())
            {
                gameplayData.SelectedItemValidation = false;
                return;
            }


            AddItem();
            gameplayData.SelectedItemValidation = true;
        }

        private void AddItem() => _selectedItemsCount++;
        
        public void CheckBasket()  => StartCoroutine(SelectedItemsController());

        private IEnumerator SelectedItemsController()
        {
            if(_selectedItemsCount == 3)
            {
                SetGatheringPos();

                yield return new WaitForSeconds(.6f);
                completeItemEvent.Raise();

                AddStar();
                ClearSelectHistory();
                ResetGameplayData();
            }
        }
        
        private void SetGatheringPos() => gameplayData.GatheringPos = gatheringPos.position;
        
        public void RemoveItem()
        {
            if (_selectedItemsCount > 1)
            {
                _selectedItemsCount--;
                return;
            }
        
        
            ClearSelectHistory();
        }
        
        private void ClearSelectHistory()
        {
            _selectedItemsCount = 0;
            _collectableItem = null;
        }

        private void AddStar()
        {
            // Get star from pooling object and get a pos to this parameter
            Vector3 pos = Camera.main.WorldToScreenPoint(gatheringPos.position);
                
            _starMovement = Instantiate(star,pos , Quaternion.identity).GetComponent<StarMovement>();
            _starMovement.gameObject.transform.SetParent(canvas);
            _starMovement.SetUIController = uIController;
            _starMovement.StartMove();
            
            _starsCount++;

            WinController();
        }
        
        public void PlayCollectParticleManager()
        {
            _collectParticleManage++;
        
            if(_collectParticleManage < 3) return;

            _collectParticleManage = 0;
            
            PlayCollectParticle();
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
        
        private void ResetGameplayData()
        {
            gameplayData.SelectedItemValidation = false;
            gameplayData.SelectedBasket = null;
            gameplayData.SelectedItemTag = null;
            gameplayData.UnUsingBasket = null;
            gameplayData.ItemTarget = Vector3.zero;
            gameplayData.GatheringPos = Vector3.zero;
        }
    }
}

