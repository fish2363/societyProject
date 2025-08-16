using System.Collections.Generic;
using System.Linq;
using Assets._03.Member.CDH.Code.GameEvents;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._03.Member.CDH.Code.Cards
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO eventChannel;
        [SerializeField] private CardsSO cardsSO;
        [SerializeField] private PoolingItemSO card;
        [SerializeField] private Transform parent;
        [SerializeField] private PoolManagerMono poolManager;

        private List<Card> cards;

        private void Awake()
        {
            cards = new List<Card>();
        }

        public void CreateCards()
        {
            foreach(CardInfo cardInfo in cardsSO.cards)
            {
                Card newCard = poolManager.Pop<Card>(card);
                newCard.SetUp(parent, cardInfo);
                newCard.ResetItem();
                newCard.DisableUI((card) => AfterSelectCard(card as Card));
                cards.Add(newCard);
            }
        }

        public void AfterSelectCard(Card selectedCard)
        {
            if(cards.Contains(selectedCard))
                cards.Remove(selectedCard);

            CardEvent cardEvent = EventEvents.OnCardEvent;
            cardEvent.cardInfo = selectedCard.CardInfo;

            eventChannel.RaiseEvent(cardEvent);

            foreach (Card card in cards)
            {
                poolManager.Push(card);
            }
        }
    }
}
