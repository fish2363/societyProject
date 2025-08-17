using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PolicyDackUIManager : MonoBehaviour
{
    [SerializeField] private PolicyCardBase policyCardPrefab;
    private PolicyCard policyCard = new PolicyCard();
    [SerializeField] private List<PolicyCardData> cardDatas = new List<PolicyCardData>();

    private void Start()
    {
        cardDatas.ForEach(cardData=> CardAdd(cardData));

    }

    public void CardAdd(PolicyCardData cardData)
    {
        policyCard.CreateCard(policyCardPrefab, cardData, this.gameObject);
    }



}
