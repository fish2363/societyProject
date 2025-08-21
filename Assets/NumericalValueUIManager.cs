using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumericalValueUIManager : MonoBehaviour
{
    [SerializeField, Header("��ȭ ǥ��")] private TextMeshProUGUI _moneyText;
    [SerializeField, Header("�α��� ǥ��")] private TextMeshProUGUI _peopleCntText;
    public Dictionary<NumericalValueType, Action<float>> valueUIDic=new();

    private void Awake()
    {
        valueUIDic = new Dictionary<NumericalValueType, Action<float>>
        {
            { NumericalValueType.Money, OnMoneyUI},
            { NumericalValueType.Happiness, OnHappinessUI},
            { NumericalValueType.PeopleCnt, OnPeopleCntUI},
            { NumericalValueType.Warming, OnWarmingUI},
        };
    }

    private void Start()
    {
        NumericalValueManager.Instance.ModifyNumericalValue(NumericalValueType.Money, ModifyType.Add, 0);
    }

    public void HandleValueChanged(NumericalValueType type, float value)
    {
        valueUIDic[type]?.Invoke(value);
    }

    private void OnMoneyUI(float value)
    {
        _moneyText.text = $"{value}��";
    }

    private void OnPeopleCntUI(float value)
    {
        _peopleCntText.text = $"{value}��";
    }

    private void OnWarmingUI(float value)
    {

    }

    private void OnHappinessUI(float value)
    {

    }
}
