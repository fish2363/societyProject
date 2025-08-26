using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PolicyCard : MonoBehaviour
{
    [SerializeField] private PolicyEffect myPolicyEffect;
    [SerializeField] private bool isSelected = false;
    [SerializeField] private bool isback = false;
    [SerializeField] private Image Icon;
    public TextMeshProUGUI CardName;
    private bool isFlipping = false;
    [SerializeField] private TextMeshProUGUI PolicyType;
    [SerializeField] private Image PolicyTypeIcon;
    [SerializeField] private TextMeshProUGUI Cost;
    [SerializeField] private Image StampIcon;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private Coroutine hoverCoroutine;
    private Tween flipTween;
    private bool isHovering = false;

    public void CreateCard(PolicyCardData policyCardData)
    {
        Icon.sprite = policyCardData.Icon;
        CardName.text = policyCardData.policyName;
        PolicyType.text = policyCardData.policyType.ToString();
        PolicyTypeIcon.sprite = policyCardData.policyTypeIcon;
        Cost.text = policyCardData.policyCost.ToString();
        myPolicyEffect = policyCardData.policyEffect;
        descriptionText.text = policyCardData.description;
    }

    public void OnClickBT()
    {
        isSelected = !isSelected;
        if (isSelected)
            SendPolicyEffect();
        else
            RemovePolicyEffect();
    }

    public void SendPolicyEffect()
    {
        StampIcon.gameObject.SetActive(true);
        PolicyDackManager.Instance.effectList.Add(myPolicyEffect);
    }

    public void RemovePolicyEffect()
    {
        StampIcon.gameObject.SetActive(false);
        PolicyDackManager.Instance.effectList.Remove(myPolicyEffect);
    }

    public void PointEnter()
    {
        isHovering = true;
        if (!isback && hoverCoroutine == null)
            hoverCoroutine = StartCoroutine(HoverDelayFlip(0.5f));
    }

    private IEnumerator HoverDelayFlip(float delay)
    {
        float timer = 0f;
        while (timer < delay)
        {
            if (!isHovering)
            {
                hoverCoroutine = null;
                yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        FlipCard(true);
        hoverCoroutine = null;
    }

    public void OnClickFlipBackButton()
    {
        if (isback && !isFlipping)
            FlipCard(false);
    }

    private void FlipCard(bool toBack)
    {
        if (isFlipping) return;

        isFlipping = true;
        isback = toBack;

        if (flipTween != null && flipTween.IsActive())
            flipTween.Kill();

        float targetY = transform.eulerAngles.y + 180f;
        flipTween = transform.DORotate(new Vector3(0, targetY, 0), 0.5f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() => { isFlipping = false; });
    }

    public void PointExit()
    {
        isHovering = false;
        if (hoverCoroutine != null)
        {
            StopCoroutine(hoverCoroutine);
            hoverCoroutine = null;
        }
    }
}
