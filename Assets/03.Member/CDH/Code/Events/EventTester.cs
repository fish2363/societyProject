using Assets._03.Member.CDH.Code.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventTester : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO eventChannelSO;
        [SerializeField] private string evtName;
        [SerializeField] private string evtDescription;

        [ContextMenu("TestEvent")]
        public void RasieEvent()
        {
            EventIssue evt = EventEvents.OnEventIssue;
            evt.evt.evtName = evtName;
            evt.evt.evtDescription = evtDescription;

            eventChannelSO.RaiseEvent(evt);
        }
    }
}
