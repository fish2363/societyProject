using System;
using System.Collections.Generic;
using UnityEngine;

public class NumericalValueManager : MonoBehaviour
{
    [SerializeField,Header("��ġ UI �Ŵ���")] private NumericalValueUIManager valueViewer;

    public Dictionary<NumericalValueType, float> numericalValueDic;

    [Header("�⺻���� ��ġ")]
    [Header("�⺻ ��� �µ�"),SerializeField, Range(-40, 70)] public float defaultWarming = 15f;
    [Header("�⺻ ���� ����"), SerializeField] public float defaultExpectedRevenue = 1000f;
    [Header("�⺻ �ŷڵ�"), SerializeField,Range(0,100)] public float defaultGlobalCredibility = 50f;
    [Header("�⺻ ����ȿ��"), SerializeField,Range(0,4)] public float defaultProductionMultiplier = 1f;

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
        #region �⺻���� �ִ� ��ġ��
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

        if (valueViewer == null) Debug.Log("NumericalValueUIManager�� �����ϴ�");
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
