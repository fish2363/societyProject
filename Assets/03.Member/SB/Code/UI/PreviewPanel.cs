using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewPanel : Singleton<PreviewPanel>
{
    //지금 만들려고 하는 건 모든 Enum의 이넘명의 텍스트 : 수치 값
    private Dictionary<NumericalValueType, float> baseStats;
    private Dictionary<NumericalValueType, float> previewStats;

    //턴 시작할때 실행
    public void ReSetBaseStats()
    {
        foreach (NumericalValueType Stat in Enum.GetValues(typeof(NumericalValueType)))
        {
            baseStats.Add(Stat, NumericalValueManager.Instance.GetNumericalValue(Stat));
            previewStats.Add(Stat, NumericalValueManager.Instance.GetNumericalValue(Stat));
        }
    }

    
    public void Start()
    {
        print(NumericalValueManager.Instance.GetNumericalValue(NumericalValueType.Warming));
       
    }


}
