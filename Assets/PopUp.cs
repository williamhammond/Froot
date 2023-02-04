using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUp : MonoBehaviour
{
    private void Awake()
    {
        transform.position = transform.position - Vector3.up * 3;
    }

    [SerializeField]
    float duration = 1f;
    [SerializeField]
    Ease easeType = Ease.Linear;

    private void Start()
    {
        StartPop();
    }
    public void StartPop()
    {
        var myTween = transform.DOMove(transform.position+Vector3.up*3, duration);
        myTween.SetEase(easeType);
        myTween.OnComplete(() =>
        finishedExpanding?.Invoke());
        myTween.Play();
    }
    public UnityEvent finishedExpanding;
}
