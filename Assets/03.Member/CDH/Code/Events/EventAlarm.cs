using Assets._03.Member.CDH.Code.GameEvent;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventAlarm : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO eventChannelSO;
        [SerializeField] private GameObject eventPrefab;

        private void Awake()
        {
            eventChannelSO.AddListener<EventIssue>(EventIssueHandler);
        }

        private void EventIssueHandler(EventIssue issue)
        {

        }
    }
}
