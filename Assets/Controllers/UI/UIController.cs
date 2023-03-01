using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Image playerAvatar;
        [SerializeField] private Image opponentAvatar;
        [SerializeField] private Image[] playerStars;
        [SerializeField] private Image[] opponentStars;
        [SerializeField] private Sprite collectedStarSprite;
        [SerializeField] private TextMeshProUGUI playerUsername;
        [SerializeField] private TextMeshProUGUI opponentUsername;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winPanel;


        private PlayerUIController _playerUIController;
        private OpponentUIController _opponentUIController;
        
        

        private void OnEnable()
        {
            Initial();
            
            _playerUIController = new PlayerUIController(playerAvatar, playerStars,collectedStarSprite, playerUsername);
            _playerUIController.PreparePlayerUI();

            _opponentUIController = new OpponentUIController(opponentAvatar, opponentStars, collectedStarSprite, opponentUsername);
            _opponentUIController.PrepareOpponentUI();
        }


        private void Initial()
        {
          winPanel.SetActive(false);
          losePanel.SetActive(false);
        }


        public void ChangePlayerStarSprite() => _playerUIController.ChangeCollectedStarSprite();
        public void ChangeOpponentStarSprite() => _opponentUIController.ChangeCollectedStarSprite();
        public void ShowWinPanel() => winPanel.SetActive(true);
        public void ShowLosePanel() => losePanel.SetActive(true);
    }
}
