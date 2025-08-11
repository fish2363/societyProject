using Assets._03.Member.CDH.Code.GameEvent;
using System;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventSystem : MonoBehaviour
    {
        [SerializeField] private EventsSO eventsSO;
        [SerializeField] private GameEventChannelSO eventChannelSO;

        public void EventIssue(string eventName, string eventDescription)
        {
            EventIssue evt = EventEvents.OnEventIssue;
            evt.evt.evtName = eventName;
            evt.evt.evtDescription = eventDescription;

            eventChannelSO.RaiseEvent(evt);
        }

        public void EventIssue(EventIssue evt)
        {
            eventChannelSO.RaiseEvent(evt);
        }
    }
}
