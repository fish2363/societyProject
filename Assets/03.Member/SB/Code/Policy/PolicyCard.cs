using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PolicyCard : MonoBehaviour
{
    [SerializeField] private PolicyEffect myPolicyEffect;
    public bool isSelected = false;
    public Image Icon;
    public TextMeshProUGUI CardName;
    public TextMeshProUGUI PolicyType;
    public Image PolicyTypeIcon;
    public TextMeshProUGUI Cost;
    public Image StampIcon;

    private void Update()
    {

      


    }
    public void CreateCard(PolicyCardData policyCardData)
    {
        Icon.sprite = policyCardData.Icon;
        CardName.text = policyCardData.policyName;
        PolicyType.text = policyCardData.policyType.ToString();
        CardName.text = policyCardData.policyName;
        PolicyTypeIcon.sprite = policyCardData.policyTypeIcon;
        Cost.text = policyCardData.policyCost.ToString();
        myPolicyEffect = policyCardData.policyEffect;

    }
    public void OnClickBT()
    {

        isSelected = !isSelected;
        if (isSelected)
            SendPolicyEffect();
        else
            RemovePolicyEffect();
    }
    public void SendPolicyEffect()
    {

        StampIcon.gameObject.SetActive(true);
        PolicyDackManager.Insstance.effectList.Add(myPolicyEffect);
    }
    public void RemovePolicyEffect()
    {
        StampIcon.gameObject.SetActive(false);
        PolicyDackManager.Insstance.effectList.Remove(myPolicyEffect);
    }
}
