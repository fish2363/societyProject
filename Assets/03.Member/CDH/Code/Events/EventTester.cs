using Assets._03.Member.CDH.Code.GameEvents;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventTester : MonoBehaviour
    {
        [SerializeField] private EVENT_TYPE testEventType;
        [SerializeField] private GameEventChannelSO eventChannelSO;

        [ContextMenu("TestEvent")]
        public void RasieEvent()
        {
            CreateEventEvent evt = EventEvents.OnCreateEvent.Initializer(testEventType);
            eventChannelSO.RaiseEvent(evt);
        }
    }
}
