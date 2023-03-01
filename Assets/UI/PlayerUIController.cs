using Data.Data.Scripts;
using Game_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerUIController
    {
        private Image _avatar;
        private Image[] _stars;
        private Sprite _collectedStar;
        private TextMeshProUGUI _username;
        private PlayerInfo _playerInfo;
        private int _collectedStarsIndex;

        public PlayerUIController(Image avatar, Image[] stars,Sprite collectedStarSprite, TextMeshProUGUI username)
        {
            _avatar = avatar;
            _stars = stars;
            _collectedStar = collectedStarSprite;
            _username = username;
            _playerInfo = DataController.Instance.GetPlayerInfo;
        }



        public void PreparePlayerUI()
        {
            _avatar.sprite = _playerInfo.Avatar;
            _username.text = _playerInfo.UserName;
        }


        public Vector3 GetStar { get => _stars[_collectedStarsIndex].transform.position; }



        public void ChangeCollectedStarSprite() => _stars[_collectedStarsIndex].sprite = _collectedStar;
    }
}
