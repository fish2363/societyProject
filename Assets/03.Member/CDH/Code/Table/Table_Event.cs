using System;
using System.Collections.Generic;

public class Table_Event : Table_Base
{
    [Serializable]
    public class EventInfo
    {
        public int Id;
        public string EventName;
        public string EventDescription;
    }

    public Dictionary<int, EventInfo> Dictionary = new Dictionary<int, EventInfo>();

    public EventInfo Get(int _Id)
    {
        if (Dictionary.ContainsKey(_Id))
            return Dictionary[_Id];

        return null;
    }
    public int GetCount()
    {
        return Dictionary.Count;
    }

    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<int, EventInfo>>(_Name, ref Dictionary);
    }

    public void Save_Binary(string _Name)
    {
        Save_Binary(_Name, Dictionary);
    }

    public void Init_Csv(string _Name, int _StartRow, int _StartCol)
    {
        CsvReader reader = GetCSVReader(_Name);

        for(int row = _StartRow; row < reader.Row; ++row)
        {
            EventInfo info = new EventInfo();

            if (Read(reader, info, row, _StartCol) == false)
                break;

            Dictionary.Add(info.Id, info);   
        }
    }

    protected bool Read(CsvReader _Reader, EventInfo _Info, int _Row, int _Col)
    {
        if (_Reader.ResetRow(_Row, _Col) == false)
            return false;

        _Reader.Get(_Row, ref _Info.Id);
        _Reader.Get(_Row, ref _Info.EventName);
        _Reader.Get(_Row, ref _Info.EventDescription);
        // 가져오는 순서 원본 txt파일의 순서랑 맞춰야 하는듯

        return true;
    }
}
