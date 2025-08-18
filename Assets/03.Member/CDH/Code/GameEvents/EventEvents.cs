using Assets._03.Member.CDH.Code.Cards;
using Assets._03.Member.CDH.Code.Events;

namespace Assets._03.Member.CDH.Code.GameEvents
{
    public static class EventEvents
    {
        readonly public static CardEvent OnCardEvent = new CardEvent();
    }

    public class CardEvent : GameEvent
    {
        public CardInfo cardInfo;
    }
}
