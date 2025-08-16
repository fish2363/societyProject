
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
        private List<EventInfo> eventInfos;
        private TableManager tableManager;
        private Table_Event eventTable;

        private void Awake()
        {
            currentAlarms = new List<EventAlarm>();
            eventInfos = new List<EventInfo>();

            tableManager = Shared.InitTableMgr();
            eventTable = tableManager.Event;

            int count = eventTable.GetCount();
            for (int i = 1; i <= count; i++)
            {
                eventInfos.Add(eventTable.Get(i));
            }
        }

        public void CreateEvent()
        {
            int num = Random.Range(0, eventInfos.Count);
            EventInfo randomEvent = eventInfos[num];

            EventAlarm newEvent = poolManager.Pop<EventAlarm>(eventPrefab);
            newEvent.SetUp(parent);

            newEvent.SetNameAndDescription(randomEvent.evtName, randomEvent.evtDescription);
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
