using Data.Data.Scripts;
using Game_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OpponentUIController
    {
        private Image _avatar;
        private Image[] _stars;
        private Sprite _collectedStar;
        private TextMeshProUGUI _username;
        private PlayerInfo _opponentInfo;
        private int _collectedStarsIndex;

        public OpponentUIController(Image avatar, Image[] stars,Sprite collectedStarSprite, TextMeshProUGUI username)
        {
            _avatar = avatar;
            _stars = stars;
            _collectedStar = collectedStarSprite;
            _username = username;
            _opponentInfo = DataController.Instance.GetOpponentInfo;
        }



        public void PrepareOpponentUI()
        {
            _avatar.sprite = _opponentInfo.Avatar;
            _username.text = _opponentInfo.UserName;
        }


        public Vector3 GetStar { get => _stars[_collectedStarsIndex].transform.position; }



        public void ChangeCollectedStarSprite() => _stars[_collectedStarsIndex].sprite = _collectedStar;
    }
}
