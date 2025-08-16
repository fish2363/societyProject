using Assets._03.Member.CDH.Code.Combat;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._03.Member.CDH.Code.Cards
{
    public class Card : CustomUI, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float movingValue;
        [SerializeField] private float duration;
        [SerializeField] private Button button;

        private Vector3 ogPosition;

        protected override void Awake()
        {
            base.Awake();

            button.onClick.AddListener(() => completionSource.SetResult());
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
