using Assets._03.Member.CDH.Code.Combat;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._03.Member.CDH.Code.Cards
{
    public class Card : CustomUI, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI cardName, cardDescription;
        [SerializeField] private float movingValue;
        [SerializeField] private float duration;

        private Vector3 ogPosition;
        private CardInfo cardInfo;

        public CardInfo CardInfo => cardInfo;

        public void SetResult()
        {
            completionSource.SetResult();
        }

        public void SetUp(Transform parent, CardInfo cardInfo)
        {
            SetUp(parent);
            this.cardInfo = cardInfo;
            cardName.text = cardInfo.name;
            cardDescription.text = cardInfo.description;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOMoveY(ogPosition.y + movingValue, duration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOMoveY(ogPosition.y, duration);
        }
    }
}
