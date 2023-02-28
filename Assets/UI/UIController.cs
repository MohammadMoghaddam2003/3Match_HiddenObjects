using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
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


        private PlayerUIController _playerUIController;
        private OpponentUIController _opponentUIController;
        
        

        private void OnEnable()
        {
            _playerUIController = new PlayerUIController(playerAvatar, playerStars,collectedStarSprite, playerUsername);
            _playerUIController.PreparePlayerUI();

            _opponentUIController = new OpponentUIController(opponentAvatar, opponentStars, collectedStarSprite, opponentUsername);
            _opponentUIController.PrepareOpponentUI();
        }


        public void ChangePlayerStarSprite() => _playerUIController.ChangeCollectedStarSprite();
        public void ChangeOpponentStarSprite() => _opponentUIController.ChangeCollectedStarSprite();
    }
}
