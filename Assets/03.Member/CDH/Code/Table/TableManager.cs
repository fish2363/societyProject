using Assets._03.Member.CDH.Code.Table;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableManager
{
    public Table_Character Character = new Table_Character();
    public Table_Card Card = new Table_Card();
    
    public void Init()
    {
#if UNITY_EDITOR
        Character.Init_Csv("Character", 1, 0);
        Card.Init_Csv("Card", 1, 0);
#else
        Character.Init_Binary("Character");
        Card.Init_Binary("Card");
#endif
    }

    public void Save()
    {
        Character.Save_Binary("Character");
        Card.Save_Binary("Card");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
