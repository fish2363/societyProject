using System.Collections.Generic;
using UnityEngine;

public class NumericalValueManager : MonoBehaviour
{
    [SerializeField,Header("수치 UI 매니저")] private NumericalValueUIManager valueViewer;

    public Dictionary<NumericalValueType, int> numericalValueDic;

    public static NumericalValueManager Instance;

    public event ValueChangedHanlder OnValueChanged;

    private void Awake()
    {
        numericalValueDic = new Dictionary<NumericalValueType, int>
        {
            { NumericalValueType.Money, 0 },
        };

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

    public int GetNumericalValue(NumericalValueType currencyType)
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
