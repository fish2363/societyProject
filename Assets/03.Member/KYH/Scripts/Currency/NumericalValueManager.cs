using System;
using System.Collections.Generic;
using UnityEngine;

public class NumericalValueManager : MonoBehaviour
{
    [SerializeField,Header("수치 UI 매니저")] private NumericalValueUIManager valueViewer;

    public Dictionary<NumericalValueType, float> numericalValueDic;

    [Header("기본적인 수치")]
    [Header("기본 평균 온도"),SerializeField, Range(-40, 70)] public float defaultWarming = 15f;
    [Header("기본 예상 세입"), SerializeField] public float defaultExpectedRevenue = 1000f;
    [Header("기본 신뢰도"), SerializeField,Range(0,100)] public float defaultGlobalCredibility = 50f;
    [Header("기본 생산효율"), SerializeField,Range(0,4)] public float defaultProductionMultiplier = 1f;

    [Space]

    public static NumericalValueManager Instance;

    public event ValueChangedHanlder OnValueChanged;

    private void Awake()
    {
        numericalValueDic = new();
        foreach(NumericalValueType valueType in Enum.GetValues(typeof(NumericalValueType)))
        {
            numericalValueDic.Add(valueType,0);
        }
        #region 기본값이 있는 수치들
        numericalValueDic[NumericalValueType.Warming] = defaultWarming;
        numericalValueDic[NumericalValueType.ExpectedRevenue] = defaultExpectedRevenue;
        
        #endregion

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (valueViewer == null) Debug.Log("NumericalValueUIManager가 없습니다");
        OnValueChanged += valueViewer.HandleValueChanged;
    }
    private void OnDestroy()
    {
        OnValueChanged -= valueViewer.HandleValueChanged;
    }

    public float GetNumericalValue(NumericalValueType currencyType)
    {
        if (!numericalValueDic.ContainsKey(currencyType))
            return 0;
        return numericalValueDic[currencyType];
    }

    public void ModifyNumericalValue(NumericalValueType valueType, ModifyType modifyType, int amount)
    {
        if (!numericalValueDic.ContainsKey(valueType))
        {
            numericalValueDic[valueType] = 0;
        }

        switch (modifyType)
        {
            case ModifyType.Set:
                numericalValueDic[valueType] = amount;
                break;
            case ModifyType.Add:
                numericalValueDic[valueType] += amount;
                break;
            case ModifyType.Multiply:
                numericalValueDic[valueType] *= amount;
                break;
            case ModifyType.Divine:
                if (amount != 0)
                    numericalValueDic[valueType] /= amount;
                break;
        }

        OnValueChanged?.Invoke(valueType, numericalValueDic[valueType]);
    }
}
