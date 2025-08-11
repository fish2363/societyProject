using TMPro;
using UnityEngine;

public class NumericalValueUIManager : MonoBehaviour
{
    [SerializeField, Header("재화 표시")] private TextMeshProUGUI _moneyText;

    public void HandleValueChanged(NumericalValueType type, int value)
    {

    }
}
