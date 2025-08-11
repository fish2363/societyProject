using Assets._03.Member.CDH.Code.GameEvent;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventAlarm : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI evtName, evtDescription;

        public void SetNameAndDescription(string name,  string description)
        {
            evtName.text = name;
            evtDescription.text = description;
        }
    }
}
