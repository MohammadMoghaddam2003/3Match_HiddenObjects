using UnityEngine;

namespace GameData.Data
{
    [CreateAssetMenu(menuName = "Data/Data Controller",fileName = "Data Controller")]
    public class DataController : ScriptableObject
    {
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private PlayerInfo opponentInfo;
        


        public PlayerInfo GetPlayerInfo { get => playerInfo; }
        public PlayerInfo GetOpponentInfo { get => opponentInfo; }

        private DataController() { }
    }
}

