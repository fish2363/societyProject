using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumericalValueUIManager : MonoBehaviour
{
    [SerializeField, Header("��ȭ ǥ��")] private TextMeshProUGUI _moneyText;
    [SerializeField, Header("�α��� ǥ��")] private TextMeshProUGUI _peopleCntText;
    public Dictionary<NumericalValueType, Action<int>> valueUIDic=new();

    private void Awake()
    {
        valueUIDic = new Dictionary<NumericalValueType, Action<int>>
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

    public void HandleValueChanged(NumericalValueType type, int value)
    {
        valueUIDic[type]?.Invoke(value);
    }

    private void OnMoneyUI(int value)
    {
        _moneyText.text = $"{value}��";
    }

    private void OnPeopleCntUI(int value)
    {
        _peopleCntText.text = $"{value}��";
    }

    private void OnWarmingUI(int value)
    {

    }

    private void OnHappinessUI(int value)
    {

    }
}
