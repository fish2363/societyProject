using Assets._03.Member.CDH.Code.GameEvent;
using System;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO eventChannelSO;
        [SerializeField] private EventAlarm eventPrefab;
        [SerializeField] private Transform canvas;

        private void Awake()
        {
            eventChannelSO.AddListener<EventIssue>(EventIssueHandler);
        }

        private void EventIssueHandler(EventIssue evt)
        {
            EventAlarm newEvent = Instantiate(eventPrefab, canvas.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform.transform);
            newEvent.SetNameAndDescription(evt.evt.evtName, evt.evt.evtDescription);
        }
    }
}
