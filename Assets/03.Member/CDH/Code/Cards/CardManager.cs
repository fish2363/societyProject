using System.Collections.Generic;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Cards
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] private PoolingItemSO card;
        [SerializeField] private Transform parent;
        [SerializeField] private PoolManagerMono poolManager;

        private List<Card> cards;

        public void CreateCards(int cnt)
        {
            for (int i = 0; i < cnt; ++i) 
            {
                Card newCard = poolManager.Pop<Card>(card);
                newCard.SetUp(parent);
                newCard.ResetItem();
                newCard.DisableUI(() =>
                {
                    if(cards.Contains(newCard))
                        cards.Remove(newCard);
                });
                cards.Add(newCard);
            }
        }
    }
}
