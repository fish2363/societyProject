using Assets._03.Member.CDH.Code.Cards;
using Assets._03.Member.CDH.Code.Events;

namespace Assets._03.Member.CDH.Code.GameEvents
{
    public static class EventEvents
    {
        readonly public static CreateEventEvent OnCreateEvent = new();
        readonly public static CardEvent OnCardEvent = new();
    }

    public class CreateEventEvent : GameEvent
    {
        public EVENT_TYPE eventType;

        public CreateEventEvent Initializer(EVENT_TYPE evtType)
        {
            eventType = evtType;
            return this;
        }
    }

    public class CardEvent : GameEvent
    {
        public CardInfo cardInfo;
    }
}
