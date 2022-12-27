using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIGunUpdater : MonoBehaviour
{
    public Image uiImage;

    [Header("Animation")]
    public float duration = .1f;
    public Ease ease = Ease.OutBack;

    private Tween _currTween;

    private void OnValidate()
    {
        if(uiImage == null)
        {
            uiImage = GetComponent<Image>();
        }
    }

    public void UpdateValue(float f)
    {
        uiImage.fillAmount = f;
    }

    public void UpdateValue(float max, float curent)
    {
        if(_currTween != null)
        {
            _currTween.Kill();
        }
        _currTween =  uiImage.DOFillAmount(1 - (curent / max), duration).SetEase(ease);
    }
}
