
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableMgr
{
    public Table_Character Character = new Table_Character();
    
    public void Init()
    {
#if UNITY_EDITOR
        //Character.Init_Csv("Character", 1, 0);
#else
        Character.Init_Binary("Character");
#endif
    }

    public void Save()
    {
        //Character.Save_Binary("Character");

        //Vector3.Distance();//거리
        //Vector3.Dot();//내적
        //Vector3.Cross();//외적
        //Vector3.Normalize();//정규화

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
