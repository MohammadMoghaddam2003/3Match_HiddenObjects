using GameData.Data;
using Game_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayAssets.UI
{
    public class PlayerUIController
    {
        private Image _avatar;
        private Image[] _stars;
        private Sprite _collectedStar;
        private TextMeshProUGUI _username;
        private PlayerInfo _playerInfo;
        private Gameplay.GameplayController _gameplayController;
        private int _collectedStarsIndex;

        public PlayerUIController(Image avatar, Image[] stars,Sprite collectedStarSprite, TextMeshProUGUI username, Gameplay.GameplayController gameplayController)
        {
            _avatar = avatar;
            _stars = stars;
            _collectedStar = collectedStarSprite;
            _username = username;
            _playerInfo = GameManager.Instance.GetPlayerInfo;
            _gameplayController = gameplayController;
        }



        public void PreparePlayerUI()
        {
            _avatar.sprite = _playerInfo.Avatar;
            _username.text = _playerInfo.UserName;
        }
        
        public Vector3 GetStar { get => _stars[_collectedStarsIndex].transform.position; }

        public void ChangeCollectedStarSprite()
        {
            _stars[_collectedStarsIndex].sprite = _collectedStar;
            PlayAnimation(_stars[_collectedStarsIndex].gameObject.GetComponent<Animation>());


            if (_collectedStarsIndex == _stars.Length - 1)
            {
                _gameplayController.PlayerWin();
                return;
            }
            
            _collectedStarsIndex++;
        }

        private void PlayAnimation(Animation starAnimator) => starAnimator.Play();
        
    }
}
