using Data.Events;
using TMPro;
using UnityEngine;

namespace Controllers.UI
{
   public class Timer : MonoBehaviour
   {
      [SerializeField] private TextMeshProUGUI timerText;
      [SerializeField] private EventSO finishedTimeEvent;
      [SerializeField] private int time = 5;


      private float _second;
      
      
      private void Update()
      {
         _second -= Time.deltaTime;
         
         
         if (_second <= 0)
         {
            if (time == 0)
            {
               finishedTimeEvent.Raise();
               this.enabled = false;
               return;
            }
            
            _second = 59;
            time--;
         }
         
         timerText.text = $"{time:00}:{_second:00}";
      }
   }
}
