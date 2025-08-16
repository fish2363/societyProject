using UnityEngine;
using UnityEngine.Events;

namespace Assets._03.Member.CDH.Code.Events
{
    public class EventTester : MonoBehaviour
    {
        public UnityEvent OnTestEventRasie;

        [ContextMenu("TestEvent")]
        public void RasieEvent()
        {
            OnTestEventRasie?.Invoke();
        }
    }
}
