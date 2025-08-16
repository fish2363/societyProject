using System;
using System.Collections.Generic;

public class Table_Character : Table_Base
{
    [Serializable]
    public class CharacterInfo
    {
        public int Id;
        public byte Type;
        public int Skill;
        public int Stat;
        public string Prefabs;
        public string Img;
        public int Name;
        public int Dec;
    }

    public Dictionary<int, CharacterInfo> Dictionary = new Dictionary<int, CharacterInfo>();

    public CharacterInfo Get(int _Id)
    {
        if (Dictionary.ContainsKey(_Id))
            return Dictionary[_Id];

        return null;
    }

    public void Init_Binary(string _Name)
    {
        Load_Binary<Dictionary<int, CharacterInfo>>(_Name, ref Dictionary);
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
            CharacterInfo info = new CharacterInfo();

            if (Read(reader, info, row, _StartCol) == false)
                break;

            Dictionary.Add(info.Id, info);   
        }
    }

    protected bool Read(CsvReader _Reader, CharacterInfo _Info, int _Row, int _Col)
    {
        if (_Reader.ResetRow(_Row, _Col) == false)
            return false;

        _Reader.Get(_Row, ref _Info.Id);
        //_Reader.Get(_Row, ref _Info.Type);
        _Reader.Get(_Row, ref _Info.Skill);
        _Reader.Get(_Row, ref _Info.Stat);
        _Reader.Get(_Row, ref _Info.Prefabs);
        _Reader.Get(_Row, ref _Info.Img);
        _Reader.Get(_Row, ref _Info.Name);
        _Reader.Get(_Row, ref _Info.Dec);
        // 가져오는 순서 원본 txt파일의 순서랑 맞춰야 하는듯

        return true;
    }
}
