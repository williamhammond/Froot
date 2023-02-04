using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Expand : MonoBehaviour
{
    float myScale;
    private void Awake()
    {
        myScale = transform.localScale.x;
        transform.localScale *= .01f;
    }

    [SerializeField]
    float duration = 1f;
    [SerializeField]
    Ease easeType = Ease.Linear;
    public void StartExpand()
    {
        var myTween = transform.DOScale(myScale, duration);
        myTween.SetEase(easeType);
        myTween.OnComplete(() =>
        finishedExpanding?.Invoke());
        myTween.Play();
    }
    public UnityEvent finishedExpanding;
}
