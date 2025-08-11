using UnityEngine;

public class KYHTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NumericalValueManager.Instance.ModifyNumericalValue(NumericalValueType.Money,ModifyType.Add,10);
        Debug.Log(NumericalValueManager.Instance.GetNumericalValue(NumericalValueType.Money));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
