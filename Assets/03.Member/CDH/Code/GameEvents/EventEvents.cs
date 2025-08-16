using Assets._03.Member.CDH.Code.Cards;
using Assets._03.Member.CDH.Code.Events;

namespace Assets._03.Member.CDH.Code.GameEvents
{
    public static class EventEvents
    {
        readonly public static EventIssue OnEventIssue = new EventIssue(); // 이벤트 시작
        readonly public static CardEvent OnCardEvent = new CardEvent(); // 이벤트 끝나서 카드 인포로 빠져 나옴.
    }

    public class EventIssue : GameEvent
    {
        public EventType evt;
    }
    public class CardEvent : GameEvent
    {
        public CardInfo cardInfo;
    }
}
