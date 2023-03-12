using System.Collections;
using Data.Events;
using Game_Manager;
using GameplayAssets.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayAssets.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Gameplay.GameplayController gameplayController;
        
        
        [Header("UI Elements")]
        [SerializeField] private Image playerAvatar;
        [SerializeField] private Image opponentAvatar;
        [SerializeField] private Image[] playerStars;
        [SerializeField] private Image[] opponentStars;
        [SerializeField] private Sprite collectedStarSprite;
        [SerializeField] private TextMeshProUGUI playerUsername;
        [SerializeField] private TextMeshProUGUI opponentUsername;
     
        
        [Header("Particles")]
        [SerializeField] private GameObject[] particleSystems;
        
        
        [Header("Panels")]
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;
        
        
        [Header("Events")]
        [SerializeField] private EventSO playAgainEvent;
        [SerializeField] private EventSO goStartEvent;


        private PlayerUIController _playerUIController;
        private AudioController _audioController;
        private OpponentUIController _opponentUIController;


        
        private void OnEnable()
        {
            Initial();
            
            _playerUIController = new PlayerUIController(playerAvatar, playerStars,collectedStarSprite, playerUsername, gameplayController);
            _playerUIController.PreparePlayerUI();

            _opponentUIController = new OpponentUIController(opponentAvatar, opponentStars, collectedStarSprite, opponentUsername, gameplayController);
            _opponentUIController.PrepareOpponentUI();
        }
        
        private void Initial()
        {
          winPanel.SetActive(false);
          losePanel.SetActive(false);

          _audioController = GameManager.Instance.GetAudioController;
        }
        
        public void ChangePlayerStarSprite() => _playerUIController.ChangeCollectedStarSprite();
        
        public void ChangeOpponentStarSprite() => _opponentUIController.ChangeCollectedStarSprite();

        public void ShowWinPanel()
        {
            winPanel.SetActive(true);
            StartCoroutine(PlayParticles());
        }

        public void ShowLosePanel() => losePanel.SetActive(true);
        
        public void PlayAgain() => playAgainEvent.Raise();
        
        public Vector3 GetPlayerStar { get => _playerUIController.GetStar; }
        
        public int GetStarsCount { get => opponentStars.Length; }
        
        public Vector3 GetTargetScale { get => playerStars[0].transform.localScale; }
        
        public void GoStart() => goStartEvent.Raise();

        private IEnumerator PlayParticles()
        {
            for (int i = 0; i < particleSystems.Length; i++)
            {
                yield return new WaitForSeconds(.2f);
                particleSystems[i].SetActive(true);
            }
        }

        public void PlayClickSound() => _audioController.Click();
    }
}
