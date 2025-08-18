using System;
using System.Collections.Generic;
using Assets._03.Member.CDH.Code.Table;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Cards
{
    public enum CARD_TYPE
    {
        Persuasive = 0,
        Pressure = 1,
        Support = 2,
    }
    
    [Serializable]
    public struct CardInfo
    {
        public string name;
        public string description;
        public CARD_TYPE cardType;

        public static implicit operator CardInfo(Table_Card.CardInfo cardInfo)
        {
            CardInfo tempInfo = new CardInfo();
            tempInfo.name = cardInfo.CardName;
            tempInfo.description = cardInfo.CardDescription;
            tempInfo.cardType = (CARD_TYPE)cardInfo.CardType;

            return tempInfo;
        }
    }
}
