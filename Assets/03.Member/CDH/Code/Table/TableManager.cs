using Assets._03.Member.CDH.Code.Table;
using System.Collections.Generic;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TableManager
{
    public Table_Event Event = new Table_Event();
    public Table_Card Card = new Table_Card();
    
    public void Init()
    {
#if UNITY_EDITOR
        Event.Init_Csv("Event", 1, 0);
        Card.Init_Csv("Card", 1, 0);
#else
        Event.Init_Binary("Event");
        Card.Init_Binary("Card");
#endif
    }

    public void Save()
    {
        Event.Save_Binary("Event");
        Card.Save_Binary("Card");

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
