using System.Collections.Generic;
using UnityEngine;

namespace Data.Events.Scripts
{
    public class EventListener : MonoBehaviour
    {
        [SerializeField] private List<EventListenerHandler> _eventListenerHandler = new List<EventListenerHandler>();

        private void OnEnable()
        {
            for (int i = 0; i < _eventListenerHandler.Count; i++)
            {
                _eventListenerHandler[i].Regester();
            }
        }
        
        private void OnDestroy()
        {
            for (int i = 0; i < _eventListenerHandler.Count; i++)
            {
                _eventListenerHandler[i].Unregester();
            }
        }
    }
}
