using System;
using System.Collections.Generic;
using UnityEngine;

    public class GameEvent
    {

    }

    [CreateAssetMenu(fileName = "GameEvenetChannel", menuName = "SO/EventChannel", order = 0)]
    public class GameEventChannelSO : ScriptableObject
    {
        private Dictionary<Type, Action<GameEvent>> _events = new Dictionary<Type, Action<GameEvent>>();
        private Dictionary<Delegate, Action<GameEvent>> _lookup = new Dictionary<Delegate, Action<GameEvent>>();

        public void AddListener<T>(Action<T> handler) where T : GameEvent
        {
            if (!_lookup.ContainsKey(handler)) // 이미 구독중인 메서드는 추가적으로 구독되지 않도록 막는다.
            {
                Action<GameEvent> castHandler = (evt) => handler(evt as T);
                _lookup[handler] = castHandler;

                Type evtType = typeof(T);
                if (_events.ContainsKey(evtType))
                {
                    _events[evtType] += castHandler;
                }
                else
                {
                    _events[evtType] = castHandler;
                }
            }
        }

        public void RemoveListener<T>(Action<T> handler) where T : GameEvent
        {
            Type evtType = typeof(T);
            if (_lookup.TryGetValue(handler, out Action<GameEvent> action))
            {
                if (_events.TryGetValue(evtType, out Action<GameEvent> internalAction))
                {
                    internalAction -= action;
                    if (internalAction == null)
                        _events.Remove(evtType);
                    else
                        _events[evtType] = internalAction;
                }
                _lookup.Remove(handler);
            }
        }

        public void RaiseEvent(GameEvent evt)
        {
            if (_events.TryGetValue(evt.GetType(), out Action<GameEvent> handlers))
                handlers?.Invoke(evt);
        }

        public void Clear()
        {
            _events.Clear();
            _lookup.Clear();
        }
    }
