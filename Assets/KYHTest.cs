using UnityEngine;
using UnityEngine.InputSystem;

public class KYHTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(NumericalValueManager.Instance.GetNumericalValue(NumericalValueType.Happiness));
    }















    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
            
            
            NumericalValueManager.Instance.ModifyNumericalValue(NumericalValueType.Happiness,ModifyType.Add,10);
            
    }
}
