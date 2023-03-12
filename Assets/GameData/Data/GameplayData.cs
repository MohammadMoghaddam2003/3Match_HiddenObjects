using System.Text;
using UnityEngine;

namespace GameData.Data
{
    [CreateAssetMenu(menuName = "Data/Gameplay Data", fileName = "Gameplay Data")]
    public class GameplayData : ScriptableObject
    {
       [SerializeField] private Transform selectedBasket;
       [SerializeField] private Transform unUsingBasket;
       [SerializeField] private Vector3 itemTarget;
       [SerializeField] private Vector3 gatheringPos;
       [SerializeField] private int itemCount;

       
        private StringBuilder _selectedItemTag;

       
        
        
        
        public StringBuilder SelectedItemTag
        { 
            get => _selectedItemTag;
            set => _selectedItemTag = value;
        }
         
         
        public Transform SelectedBasket
        {
            get => selectedBasket;
            set => selectedBasket = value;
        }
        
        
        public Transform UnUsingBasket
        {
            get => unUsingBasket;
            set => unUsingBasket = value;
        }
        
        
        public Vector3 ItemTarget
        {
            get => itemTarget;
            set => itemTarget = value;
        }
        
        
        public Vector3 GatheringPos
        {
            get => gatheringPos;
            set => gatheringPos = value;
        }
        
        
        public int GetItemCount { get => itemCount; }
    }
}
