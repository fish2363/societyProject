using UnityEngine;

public class PolicyCard : MonoBehaviour
{

    public void CreateCard(PolicyCardBase cardBase,PolicyCardData policyCardData, GameObject parent)
    {
        cardBase.Icon.sprite = policyCardData.Icon;
        cardBase.CardName.text = policyCardData.policyName;
        cardBase.PolicyType.text = policyCardData.policyType.ToString();
        cardBase.CardName.text = policyCardData.policyName;
        cardBase.PolicyTypeIcon.sprite = policyCardData.policyTypeIcon;
        cardBase.Cost.text = policyCardData.policyCost.ToString();
        Instantiate(cardBase, parent.transform);
    }


}
