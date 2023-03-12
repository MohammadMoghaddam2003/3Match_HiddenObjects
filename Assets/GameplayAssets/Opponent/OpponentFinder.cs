using System;
using GameplayAssets.Audio;
using GameData.Data;
using Game_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameplayAssets.Opponent
{
    public class OpponentFinder : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image playerAvatar;
        [SerializeField] private Image opponentAvatar;
        [SerializeField] private Sprite[] maleAvatars;
        [SerializeField] private Sprite[] femaleAvatars;
        [SerializeField] private GameObject loadingImage;
        [SerializeField] private GameObject opponentUI;
        [SerializeField] private TextMeshProUGUI playerUsername;
        [SerializeField] private TextMeshProUGUI opponentUsername;
        
        [Header("Information")]
        [SerializeField] private String[] maleNames;
        [SerializeField] private String[] femaleNames;


        private PlayerInfo _playerInfo;
        private PlayerInfo _opponentInfo;
        private AudioController _audioController;


        private void Start()
        {
            Initial();
            PlacementUIElements();
            FindOpponent();
        }

        private void Initial()
        {
            opponentUI.SetActive(false);
            loadingImage.SetActive(true);
            
            _playerInfo = GameManager.Instance.GetPlayerInfo;
            _opponentInfo = GameManager.Instance.GetOpponentInfo;
            _audioController = GameManager.Instance.GetAudioController;
        }
        
        private void PlacementUIElements()
        {
            playerAvatar.sprite = _playerInfo.Avatar;
            playerUsername.text = _playerInfo.UserName;
        }

        private void FindOpponent()
        {
            if (RandomGender() == PlayersGender.Male)
            {
                int nameIndex = Random.Range(0, maleNames.Length);
                int avatarIndex = Random.Range(0, maleAvatars.Length);

                opponentUsername.text = maleNames[nameIndex];
                opponentAvatar.sprite = maleAvatars[avatarIndex];
            }
            else
            {
                int nameIndex = Random.Range(0, femaleNames.Length);
                int avatarIndex = Random.Range(0, femaleAvatars.Length);

                opponentUsername.text = femaleNames[nameIndex];
                opponentAvatar.sprite = femaleAvatars[avatarIndex];
            }
            
            
            SetOpponentData();
        }
        
        private PlayersGender RandomGender()
        {
            int randomGender = Random.Range(1, 3);
            
            if (randomGender == 1) return PlayersGender.Male;
            return PlayersGender.Female; 
        }

        private void SetOpponentData()
        {
            _opponentInfo.Avatar = opponentAvatar.sprite;
            _opponentInfo.UserName = opponentUsername.text;
        }
        
        public void ShowOpponent()
        {
            loadingImage.SetActive(false);
            opponentUI.SetActive(true);
            _audioController.FindOpponent();    
        }
    }

    enum PlayersGender
    {
        Male,
        Female
    }
}
