using Data.Data;
using UnityEngine;

namespace Controllers.Data
{
    [CreateAssetMenu(menuName = "Data/Data Controller",fileName = "Data Controller")]
    public class DataController : ScriptableObject
    {
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private PlayerInfo opponentInfo;
        [SerializeField] private GameplayData gameplayData;

        

    
        public PlayerInfo GetPlayerInfo { get => playerInfo; }
        public PlayerInfo GetOpponentInfo { get => opponentInfo; }
        public GameplayData GetGameplayData { get => gameplayData; }



        protected DataController() { }
    }
}

