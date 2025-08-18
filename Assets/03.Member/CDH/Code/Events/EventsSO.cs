using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Events
{
    [Serializable]
    public struct EventInfo
    {
        public string evtName;
        public string evtDescription;

        public static implicit operator EventInfo(Table_Event.EventInfo eventInfo)
        {
            EventInfo tempInfo = new EventInfo();
            tempInfo.evtName = eventInfo.EventName;
            tempInfo.evtDescription = eventInfo.EventDescription;

            return tempInfo;
        }
    }
}
