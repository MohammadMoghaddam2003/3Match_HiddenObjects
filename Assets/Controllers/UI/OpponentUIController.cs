using Data.Data;
using Game_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
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
            _opponentInfo = GameManager.Instance.GetOpponentInfo;
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
                GameManager.Instance.GetLevelController.PlayerLose();
                return;
            }
            
            _collectedStarsIndex++;
        }


        private void PlayAnimation(Animation starAnimation) => starAnimation.Play();
    }
}
