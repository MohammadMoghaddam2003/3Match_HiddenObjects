using Data.Data.Scripts;
using Game_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Scripts
{
    public class SignInController : MonoBehaviour
    {
        [SerializeField] private MenuController menuController;
        [SerializeField] private TextMeshProUGUI profileUserNameText;
        [SerializeField] private Image profileAvatar;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Scrollbar scrollbar;
        [SerializeField] private Image playButton;
        [SerializeField] private Color playingButtonColor;
        [SerializeField] private Color unPlayingButtonColor;
        [SerializeField] private int maxUserNameCharacter = 10;


        private PlayerInfo _playerInfo;
        private bool _canPlay;





        private void Start() => InitialSetting();


        private void InitialSetting()
        {
            _playerInfo = GameManager.Instance.GetPlayerInfo;
            scrollbar.value = 0;
            playButton.color = unPlayingButtonColor;
            _canPlay = false;
        }


        private void SetCanPlay()
        {
            if(_canPlay) return;

            _canPlay = true;
            PlayButtonChangeColor(playingButtonColor);
        }
        
        
        private void SetCantPlay()
        {
            _canPlay = false;
            PlayButtonChangeColor(unPlayingButtonColor);
        }

        private void PlayButtonChangeColor(Color color) => playButton.color = color;


        private bool CheckUserNameValidation(string userName)
        {
            char[] userNameChar = userName.ToCharArray();

            if (userNameChar.Length > 0 && userNameChar.Length <= maxUserNameCharacter)
            {
                SetCanPlay();
                return true;
            }
            else if (userNameChar.Length == 0)
            {
                SetCantPlay();
                return true;
            }
            else return false;
        }
        

        public void OnChangeUserName()
        {
            if (CheckUserNameValidation(inputField.text))
            {
                profileUserNameText.text = inputField.text;
            }
            else
            {
                inputField.text = profileUserNameText.text;
            }

        } 


        public void OnChangeAvatar(Image avatar) => profileAvatar.sprite = avatar.sprite;
        

        public void SignIn()
        {
            if(!_canPlay) return;
            
            _playerInfo.UserName = profileUserNameText.text;
            _playerInfo.Avatar = profileAvatar.sprite;
            menuController.GoLoading();
        }
    }
}
