using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    // Update is called once per frame
    Sequence mySequence;

    private void OnEnable()
    {
        StartSequence();
    }

    void StartSequence()
    {
        mySequence = DOTween.Sequence();
        var upTween = transform.DOMoveY(transform.position.y + 1f, 1f);
        upTween.SetEase(Ease.InOutSine);
        upTween.SetLoops(-1,LoopType.Yoyo);
        mySequence.Append(upTween);
        var rotTween = transform.DORotate(transform.rotation.eulerAngles, 1f, RotateMode.FastBeyond360);
        rotTween.SetLoops(-1);
        mySequence.Join(rotTween);
        mySequence.Play();
        //mySequence.OnComplete(() => StartSequence());
    }

    private void OnDisable()
    {
        if(mySequence!=null)
        {
            mySequence.Complete();
        }
    }
}
