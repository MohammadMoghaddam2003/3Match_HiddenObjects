using System;
using UnityEngine;

namespace Data.Data.Scripts
{
    [CreateAssetMenu(menuName = "Data/Data Controller",fileName = "Data Controller")]
    public class DataController : ScriptableObject
    {
        [SerializeField] private PlayerInfo playerInfo;
        [SerializeField] private PlayerInfo opponentInfo;
        [SerializeField] private GameplayData gameplayData;




        public static DataController Instance;

        public PlayerInfo GetPlayerInfo { get => playerInfo; }
        public PlayerInfo GetOpponentInfo { get => opponentInfo; }
        public GameplayData GetGameplayData { get => gameplayData; }



        protected DataController() { }

        private void OnEnable() => Instance = this;
    }
}
