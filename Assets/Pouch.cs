using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pouch : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    GameObject shakeable;
    public void SetCount(int count)
    {
        text.text = count.ToString();
        Pulse();
    }
    Tween myTween;

    void Pulse()
    {
        if(myTween!=null)
        {
            myTween.Complete();
        }
        myTween = shakeable.transform.DOShakeScale(1, .6f,8,45);
        myTween.SetEase(Ease.InOutSine);
        myTween.Play();
    }

}
