using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "PollicyCard", menuName = "PollicyCardData")]
public class PolicyCardData : ScriptableObject
{
    public string policyName;
    public float policyCost;
    public PolicyType policyType;
    public PolicyEffectBase policyEffect;
    public Sprite policyTypeIcon;
    public Sprite Icon;

    public string description;
}

public enum PolicyType
{
    Eco,
    Industry
}