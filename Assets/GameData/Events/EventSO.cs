using System.Collections.Generic;
using UnityEngine;

namespace Data.Events
{
    
    [CreateAssetMenu(menuName = "Data/Event", fileName = "New Event")]
    public class EventSO : ScriptableObject
    {
        private List<EventListenerHandler> _eventListenerHandler = new List<EventListenerHandler>();

        public void Raise()
        {
            for (int i = 0; i < _eventListenerHandler.Count; i++)
            {
                _eventListenerHandler[i].ListenerRaise();
            }
        }

        public void RegesterListener(EventListenerHandler eventListenerHandler)
        {
            if (!_eventListenerHandler.Contains(eventListenerHandler))
            {
                _eventListenerHandler.Add(eventListenerHandler);
            }
        }
    
    
        public void UnregesterListener(EventListenerHandler eventListenerHandler)
        {
            if (_eventListenerHandler.Contains(eventListenerHandler))
            {
                _eventListenerHandler.Remove(eventListenerHandler);
            }
        }
    }
}
