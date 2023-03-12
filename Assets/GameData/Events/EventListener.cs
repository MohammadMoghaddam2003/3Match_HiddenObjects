using System.Collections.Generic;
using UnityEngine;

namespace Data.Events
{
    public class EventListener : MonoBehaviour
    {
        [SerializeField] private List<EventListenerHandler> eventListenerHandler = new List<EventListenerHandler>();

        private void OnEnable()
        {
            for (int i = 0; i < eventListenerHandler.Count; i++)
            {
                eventListenerHandler[i].Regester();
            }
        }
        
        private void OnDestroy()
        {
            for (int i = 0; i < eventListenerHandler.Count; i++)
            {
                eventListenerHandler[i].Unregester();
            }
        }
    }
}
