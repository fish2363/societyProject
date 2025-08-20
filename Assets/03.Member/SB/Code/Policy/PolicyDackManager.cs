using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets._03.Member.CDH.Code.Cards;
using Unity.VisualScripting;

public class PolicyDackManager : Singleton<PolicyDackManager>
{
    [SerializeField] private PolicyCard policyCardPrefab;
    
    [SerializeField] private List<PolicyCardData> cardDatas = new List<PolicyCardData>();
    [SerializeField] private List<PolicyCard> policyCards = new List<PolicyCard>();
    public List<PolicyEffect> effectList = new List<PolicyEffect>();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ApplyPolicyEffects();
        }
    }
    private void Start()
    {
        foreach (PolicyCardData cardData in cardDatas)
            CardAdd(cardData);
    }

    public void CardAdd(PolicyCardData cardData)
    {
        bool canAdd = policyCards.Exists(card => card.CardName.text == cardData.policyName);
        if (canAdd)
            return;

        PolicyCard newCard = Instantiate(policyCardPrefab, transform);
        newCard.CreateCard(cardData);
        policyCards.Add(newCard);
    }

    public void ApplyPolicyEffects()
    {
        effectList.ForEach(effect => effect.ApplyEffect());
    }


}
