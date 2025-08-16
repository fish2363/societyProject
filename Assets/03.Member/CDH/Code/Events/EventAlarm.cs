
using Assets._03.Member.CDH.Code.Combat;
using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventAlarm : CustomUI
    {
        [SerializeField] private TextMeshProUGUI evtName, evtDescription;
        [SerializeField] private Button button;

        public bool isOpen { get; set; }

        protected override void Awake()
        {
            button.onClick.AddListener(() => completionSource.SetResult());
        }

        public override void ResetItem()
        {
            base.ResetItem();

            transform.position = Vector3.zero;
            isOpen = false;
        }

        public void SetNameAndDescription(string name,  string description)
        {
            evtName.text = name;
            evtDescription.text = description;
        }
    }
}
