using GameData.Data;
using Game_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayAssets.UI
{
    public class OpponentUIController
    {
        private Image _avatar;
        private Image[] _stars;
        private Sprite _collectedStar;
        private TextMeshProUGUI _username;
        private PlayerInfo _opponentInfo;
        private Gameplay.GameplayController _gameplayController;
        private int _collectedStarsIndex;

        public OpponentUIController(Image avatar, Image[] stars,Sprite collectedStarSprite, TextMeshProUGUI username, Gameplay.GameplayController gameplayController)
        {
            _avatar = avatar;
            _stars = stars;
            _collectedStar = collectedStarSprite;
            _username = username;
            _opponentInfo = GameManager.Instance.GetOpponentInfo;
            _gameplayController = gameplayController;
        }
        
        

        public void PrepareOpponentUI()
        {
            _avatar.sprite = _opponentInfo.Avatar;
            _username.text = _opponentInfo.UserName;
        }
        
        public void ChangeCollectedStarSprite()
        {
            _stars[_collectedStarsIndex].sprite = _collectedStar;
            PlayAnimation(_stars[_collectedStarsIndex].gameObject.GetComponent<Animation>());


            if (_collectedStarsIndex == _stars.Length - 1)
            {
                _gameplayController.PlayerLose();
                return;
            }
            
            _collectedStarsIndex++;
        }
        
        private void PlayAnimation(Animation starAnimation) => starAnimation.Play();
    }
}
