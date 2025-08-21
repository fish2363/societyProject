using System;
using System.Collections.Generic;

namespace Assets._03.Member.CDH.Code.Table
{
    public class Table_Card : Table_Base
    {
        [Serializable]
        public class CardInfo
        {
            public int Id;
            public string CardName;
            public string CardDescription;
            public int EventType;
            public int CardType;
        }

        public Dictionary<int, CardInfo> Dictionary = new Dictionary<int, CardInfo>();

        public CardInfo Get(int _Id)
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
            Load_Binary<Dictionary<int, CardInfo>>(_Name, ref Dictionary);
        }

        public void Save_Binary(string _Name)
        {
            Save_Binary(_Name, Dictionary);
        }

        public void Init_Csv(string _Name, int _StartRow, int _StartCol)
        {
            CsvReader reader = GetCSVReader(_Name);

            for (int row = _StartRow; row < reader.Row; ++row)
            {
                CardInfo info = new CardInfo();

                if (Read(reader, info, row, _StartCol) == false)
                    break;

                Dictionary.Add(info.Id, info);
            }
        }

        protected bool Read(CsvReader _Reader, CardInfo _Info, int _Row, int _Col)
        {
            if (_Reader.ResetRow(_Row, _Col) == false)
                return false;

            _Reader.Get(_Row, ref _Info.Id);
            _Reader.Get(_Row, ref _Info.CardName);
            _Reader.Get(_Row, ref _Info.CardDescription);
            _Reader.Get(_Row, ref _Info.EventType);
            _Reader.Get(_Row, ref _Info.CardType);

            return true;
        }
    }
}
