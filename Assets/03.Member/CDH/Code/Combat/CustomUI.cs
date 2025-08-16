using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Combat
{
    public abstract class CustomUI : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolingItemSO PoolingType { get; private set; }

        public GameObject GameObject => gameObject;
        
        protected Pool myPool;
        protected AwaitableCompletionSource completionSource;

        protected virtual void Awake()
        {
            completionSource = new AwaitableCompletionSource();
        }

        public virtual void SetUp(Transform parent)
        {
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }

        public virtual void ResetItem()
        {
            completionSource.Reset();
        }

        public virtual void SetUpPool(Pool pool)
        {
            myPool = pool;
        }

        public virtual async void DisableUI(Action<CustomUI> handler)
        {
            await completionSource.Awaitable;
            myPool.Push(this);
            handler?.Invoke(this);
        }
    }
}
