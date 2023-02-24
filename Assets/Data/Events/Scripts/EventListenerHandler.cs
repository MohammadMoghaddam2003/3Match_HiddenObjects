using System;
using UnityEngine;
using UnityEngine.Events;

namespace Data.Events.Scripts
{
    
    [Serializable]
    public class EventListenerHandler
    {
        [SerializeField] private EventSO eventSO;
        [SerializeField] private UnityEvent reponse;


        public void Regester() => eventSO.RegesterListener(this);
        
        
        public void Unregester() => eventSO.UnregesterListener(this);
        
        public void ListenerRaise() => reponse.Invoke();
    }
}
