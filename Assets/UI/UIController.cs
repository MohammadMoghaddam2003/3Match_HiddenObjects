using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Image playerAvatar;
        [SerializeField] private Image[] playerStars;
        [SerializeField] private Sprite collectedStarSprite;
        [SerializeField] private TextMeshProUGUI playerUsername;


        private PlayerUIController _playerUIController;

        private void OnEnable()
        {
            _playerUIController = new PlayerUIController(playerAvatar, playerStars,collectedStarSprite, playerUsername);
            _playerUIController.PreparePlayerUI();
        }


        public void ChangePlayerStarSprite() => _playerUIController.ChangeCollectedStarSprite();
    }
}
