using Assets._03.Member.CDH.Code.Events;

namespace Assets._03.Member.CDH.Code.GameEvents
{
    public static class EventEvents
    {
        readonly public static EventIssue OnEventIssue = new EventIssue(); // 이벤트 시작
    }

    public class EventIssue : GameEvent
    {
        public EventType evt;
    }
}
