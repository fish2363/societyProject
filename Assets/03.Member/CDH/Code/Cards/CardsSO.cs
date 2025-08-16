using System;
using System.Collections.Generic;
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
    }

    [CreateAssetMenu(fileName = "CardsSO", menuName = "SO/Card")]
    public class CardsSO : ScriptableObject
    {
        public List<CardInfo> cards;
    }
}
