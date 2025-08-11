using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Assets._03.Member.CDH.Code
{
    [CreateAssetMenu(fileName = "EventsSO", menuName = "SO/EventsSO")]
    public class EventsSO : ScriptableObject
    {
        public List<EventType> events;
    }

    [Serializable]
    public struct EventType
    {
        public string evtName;
        public string evtDescription;
    }
}
