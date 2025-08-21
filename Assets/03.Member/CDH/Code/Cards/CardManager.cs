using System.Collections.Generic;
using Assets._03.Member.CDH.Code.Events;
using Assets._03.Member.CDH.Code.GameEvents;
using Assets._03.Member.CDH.Code.Table;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Cards
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO eventChannel;
        [SerializeField] private PoolingItemSO card;
        [SerializeField] private Transform parent;
        [SerializeField] private PoolManagerMono poolManager;

        private List<CardInfo> cardInfos;
        private List<Card> currentCards;
        private Table_Card cardTable;
        private TableManager tableManager;

        private void Awake()
        {
            cardInfos = new List<CardInfo>();
            currentCards = new List<Card>();

            tableManager = Shared.InitTableMgr();
            cardTable = tableManager.Card;

            int count = cardTable.GetCount();
            for (int i = 1; i <= count; i++)
            {
                cardInfos.Add(cardTable.Get(i));
            }
        }

        public void CreateCards(EVENT_TYPE evtType)
        {
            foreach (CardInfo cardInfo in cardInfos)
            {
                if(evtType == cardInfo.eventType)
                {
                    Card newCard = poolManager.Pop<Card>(card);
                    newCard.SetUp(parent, cardInfo);
                    newCard.ResetItem();
                    newCard.DisableUI((card) => AfterSelectCard(card as Card));
                    currentCards.Add(newCard);
                }
            }
        }

        public void AfterSelectCard(Card selectedCard)
        {
            CardEvent cardEvent = EventEvents.OnCardEvent;
            cardEvent.cardInfo = selectedCard.CardInfo;

            eventChannel.RaiseEvent(cardEvent);
        }
    }
}
