
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
    public class EventAlarm : MonoBehaviour, IPoolable
    {
        [SerializeField] private TextMeshProUGUI evtName, evtDescription;
        [SerializeField] private Button button;

        [field: SerializeField] public PoolingItemSO PoolingType { get; set; }

        public GameObject GameObject => gameObject;
        public bool isOpen { get; set; }

        private AwaitableCompletionSource completionSource;
        private Pool myPool;

        private void Awake()
        {
            button.onClick.AddListener(() => completionSource.SetResult());
        }

        public void SetUpPool(Pool pool)
        {
            myPool = pool;
        }

        public void ResetItem()
        {
            completionSource = new AwaitableCompletionSource();
            transform.position = Vector3.zero;
            isOpen = false;
        }

        public void SetNameAndDescription(string name,  string description)
        {
            evtName.text = name;
            evtDescription.text = description;
        }

        public async void DestroyEventAlarm(Action handler)
        {
            await completionSource.Awaitable;
            myPool.Push(this);
            handler?.Invoke();
        }
    }
}
