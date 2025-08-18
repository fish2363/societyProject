
using Assets._03.Member.CDH.Code.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventAlarm : CustomUI
    {
        [SerializeField] private TextMeshProUGUI evtName, evtDescription;

        public bool isOpen { get; set; }

        public void SetResult()
        {
            completionSource.SetResult();
        }

        public override void ResetItem()
        {
            base.ResetItem();

            transform.position = Vector3.zero;
            isOpen = false;
        }

        public void SetNameAndDescription(string name, string description)
        {
            evtName.text = name;
            evtDescription.text = description;
        }
    }
}
