using Data.Data;
using Gameplay_Assets.Gameplay_Controller;
using GameplayAssets.GameplayController;
using UnityEngine;

namespace Controllers.Basket
{
    public class BasketManager : MonoBehaviour
    {
        [SerializeField] private Baskets[] baskets;
        [SerializeField] private GameplayData gameplayData;


        private int _basketIndex;
        
        
        
        
    
        public void PrepareBasket()
        {
            if(!gameplayData.SelectedItemValidation) return;
            
            
            for (_basketIndex = 0; _basketIndex < baskets.Length; _basketIndex++)
            {
                if (!baskets[_basketIndex].GetUsed)
                {
                    gameplayData.SelectedBasket = baskets[_basketIndex].GetParent;
                    gameplayData.ItemTarget = baskets[_basketIndex].GetPos;
                    SetBasketUsing(baskets[_basketIndex]);  
                    break;
                }
            }   
        }


        public void BasketUnUse()
        {
            for (int i = 0; i < baskets.Length; i++)
            {
                if (gameplayData.UnUsingBasket == baskets[i].GetParent)
                {
                    SetBasketUnUsing(baskets[i]);
                    break;
                }
            }
        }

        private void SetBasketUnUsing(Baskets basket) => basket.SetUsedFalse();

        private void SetBasketUsing(Baskets basket) => basket.SetUsedTrue();
    
    
        public void ClearBasketsUsing()
        {
            for (int i = 0; i < baskets.Length; i++)
            {
                baskets[i].SetUsedFalse();
            }
        }
    }
}

