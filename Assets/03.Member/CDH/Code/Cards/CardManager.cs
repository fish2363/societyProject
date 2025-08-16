using System.Collections.Generic;
using System.Linq;
using Assets._03.Member.CDH.Code.GameEvents;
using Assets._03.Member.CDH.Code.Table;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static Assets._03.Member.CDH.Code.Table.Table_Card;

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
            for(int i = 1; i <= count; i++)
            {
                cardInfos.Add(cardTable.Get(i));
            }
        }

        public void CreateCards()
        {
            foreach(CardInfo cardInfo in cardInfos)
            {
                Card newCard = poolManager.Pop<Card>(card);
                newCard.SetUp(parent, cardInfo);
                newCard.ResetItem();
                newCard.DisableUI((card) => AfterSelectCard(card as Card));
                currentCards.Add(newCard);
            }
        }

        public void AfterSelectCard(Card selectedCard)
        {
            if(currentCards.Contains(selectedCard))
                currentCards.Remove(selectedCard);

            CardEvent cardEvent = EventEvents.OnCardEvent;
            cardEvent.cardInfo = selectedCard.CardInfo;

            eventChannel.RaiseEvent(cardEvent);

            foreach (Card card in currentCards)
            {
                currentCards.Remove(card);
                poolManager.Push(card);
            }
        }
    }
}
