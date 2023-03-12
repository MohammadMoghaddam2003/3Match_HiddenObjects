using System.Collections;
using System.Text;
using GameplayAssets.Audio;
using GameplayAssets.Basket;
using GameplayAssets.Level;
using GameplayAssets.UI;
using GameData.Data;
using Game_Manager;
using GameplayAssets.Object_Pool;
using GameplayAssets.Star;
using Unity.VisualScripting;
using UnityEngine;

namespace GameplayAssets.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private Transform gatheringPos;
        [SerializeField] private ParticleSystem collectParticleSystem;

        [Header("Data")]
        [SerializeField] private GameplayData gameplayData;
        
        [Header("Controllers")]
        [SerializeField] private UIController uIController;
        [SerializeField] private BasketManager basketManager;

        [Header("Star Fields")] 
        [SerializeField] private ObjectPool objectPool;
        [SerializeField] private Transform starInitialPos;
        
        
        private static StringBuilder _collectableItem;

        private StarMovement _starMovement;
        private AudioController _audioController;
        private LevelController _levelController;
        private int _selectedItemsCount;
        private int _collectParticleManage;


        public CompleteCollection CompleteCollection;


        
        private void Awake() => ResetGameplayData();

        private void Start()
        {
            _audioController = GameManager.Instance.GetAudioController;
            _levelController = GameManager.Instance.GetLevelController;
            PlayMusic();
        }

        public bool SelectedItem()
        {
             _collectableItem ??= gameplayData.SelectedItemTag;
            
            if (gameplayData.SelectedItemTag.ToString() != _collectableItem.ToString())
            {
                PlayWrongClickObjectSound();
                return false;
            }


            AddItem();
            PlayCollectObjectSound();
            PrepareBasket();
            return true;
        }
        
        private void PrepareBasket() => basketManager.PrepareBasket();
        
        private void AddItem() => _selectedItemsCount++;
        
        public void CheckBasket()  => StartCoroutine(SelectedItemsController());

        private IEnumerator SelectedItemsController()
        {
            if(_selectedItemsCount == 3)
            {
                SetGatheringPos();

                yield return new WaitForSeconds(.6f);

                basketManager.ClearBasketsUsing();
                CompleteCollection();
                PlayCompleteSound();

                AddStar();
                ClearSelectHistory();
                ResetGameplayData();
            }
        }

        private void PlayMusic() => _audioController.PlayMusic(); 
        
        private void PlayCompleteSound() => _audioController.CompleteCollect(); 
        
        public void PlayWrongClickObjectSound() => _audioController.WrongClickObject(); 
        
        public void PlayReturnObjectSound() => _audioController.ReturnObject(); 
        
        private void PlayCollectObjectSound() => _audioController.CollectObject(); 

        private void SetGatheringPos() => gameplayData.GatheringPos = gatheringPos.position;
        
        public void RemoveItem()
        {
            basketManager.BasketUnUse();

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
            _starMovement = objectPool.GetStar.GetComponent<StarMovement>();
            _starMovement.transform.position = starInitialPos.position;
            _starMovement.SetDefault();
            _starMovement.SetUIController = uIController;
            _starMovement.SetObjectPool = objectPool;
            _starMovement.StartMove();
        }
        
        public void PlayCollectParticleManager()
        {
            _collectParticleManage++;
        
            if(_collectParticleManage < 3) return;

            _collectParticleManage = 0;
            
            PlayCollectParticle();
        }
        
        public void PlayerWin() => _levelController.PlayerWin();

        public void PlayerLose() => _levelController.PlayerLose();
        
        private void PlayCollectParticle() => collectParticleSystem.Play();
        
        private void ResetGameplayData()
        {
            gameplayData.SelectedBasket = null;
            gameplayData.SelectedItemTag = null;
            gameplayData.UnUsingBasket = null;
            gameplayData.ItemTarget = Vector3.zero;
            gameplayData.GatheringPos = Vector3.zero;
        }

        public void Disabler() => gameObject.SetActive(false);
    }
    
    public delegate void CompleteCollection();
}

