
using Assets._03.Member.CDH.Code.GameEvents;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO eventChannelSO;
        [SerializeField] private EventAlarm eventPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private Transform position;
        [SerializeField] private float alarmDuration;
        [SerializeField] private float alarmDeadDuration;
        [Range(350, 400), SerializeField] private float alarmEndValue;

        private List<EventAlarm> currentAlarms;

        private void Awake()
        {
            currentAlarms = new List<EventAlarm>();

            eventChannelSO.AddListener<EventIssue>(EventIssueHandler);
        }

        private void EventIssueHandler(EventIssue evt)
        {
            EventAlarm newEvent = Instantiate(eventPrefab, position.position, Quaternion.identity, parent);
            newEvent.SetNameAndDescription(evt.evt.evtName, evt.evt.evtDescription);
            DOVirtual.DelayedCall(alarmDeadDuration, () =>
            {
                Destroy(newEvent.gameObject);
                currentAlarms.Remove(newEvent);
            });

            currentAlarms.Add(newEvent);

            currentAlarms.ForEach(alarm =>
            {
                alarm.transform.DOMoveY(alarm.transform.position.y - alarmEndValue, alarmDuration, true);
            });
        }
    }
}
