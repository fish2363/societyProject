using UnityEngine;

[CreateAssetMenu(fileName = "PollicyCard", menuName = "PollicyCard")]
public class GreenFacilitySubsidy : PolicyEffect
{
    public override void ApplyEffect()
    {
        Debug.Log("친환경 산업 보조금 지원");
    }
}
