using System;
using System.Collections.Generic;
using Assets._03.Member.CDH.Code.Events;
using Assets._03.Member.CDH.Code.Table;
using UnityEngine;

namespace Assets._03.Member.CDH.Code.Cards
{
    public enum CARD_TYPE
    {
        YES = 0,
        NO = 1,
        Persuasive = 2,
        Pressure = 3,
        Support = 4,
    }
    
    [Serializable]
    public struct CardInfo
    {
        public string name;
        public string description;
        public EVENT_TYPE eventType;
        public CARD_TYPE cardType;

        public static implicit operator CardInfo(Table_Card.CardInfo cardInfo)
        {
            CardInfo tempInfo = new CardInfo();
            tempInfo.name = cardInfo.CardName;
            tempInfo.description = cardInfo.CardDescription;
            tempInfo.eventType = (EVENT_TYPE)cardInfo.EventType;
            tempInfo.cardType = (CARD_TYPE)cardInfo.CardType;

            return tempInfo;
        }
    }
}
