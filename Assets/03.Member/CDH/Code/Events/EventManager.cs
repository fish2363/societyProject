
using Assets._03.Member.CDH.Code.GameEvents;
using DG.Tweening;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventManager : MonoBehaviour
    {
        public UnityEvent OnAlarmSelect;

        [SerializeField] private GameEventChannelSO eventChannel;
        [SerializeField] private PoolingItemSO eventPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private float alarmDuration;
        [SerializeField] private Transform alarmEndValue;
        [SerializeField] private PoolManagerMono poolManager;

        private List<EventAlarm> currentAlarms;

        private void Awake()
        {
            currentAlarms = new List<EventAlarm>();

            eventChannel.AddListener<EventIssue>(EventIssueHandler);
        }

        private void EventIssueHandler(EventIssue evt)
        {
            EventAlarm newEvent = poolManager.Pop<EventAlarm>(eventPrefab);
            newEvent.SetUp(parent);

            newEvent.SetNameAndDescription(evt.evt.evtName, evt.evt.evtDescription);
            newEvent.DisableUI((evt) =>
            {
                currentAlarms.Remove(evt as EventAlarm);
                OnAlarmSelect?.Invoke();
            });

            currentAlarms.Add(newEvent);

            int sibling = currentAlarms.Count - 1;
            currentAlarms.ForEach(alarm =>
            {
                if(alarm.isOpen)
                    alarm.transform.DOMoveY(alarm.transform.position.y - 30f, alarmDuration);
                else
                {
                    alarm.isOpen = true;
                    alarm.transform.DOMoveY(alarm.transform.position.y - Mathf.Abs(parent.position.y - alarmEndValue.position.y), alarmDuration);
                }
                alarm.gameObject.name = sibling.ToString();
                alarm.transform.SetSiblingIndex(sibling);
                sibling--;
            });
        }
    }
}
