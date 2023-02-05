using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWalk : MonoBehaviour
{
    [SerializeField]
    AnimationCurve walkPattern, heightCurve,durationCurve;
    
    [SerializeField]
    float previousSpeed = 0f;
    
    private bool isMoving = false;
    
    [SerializeField]
    float speed = 0f;

    [SerializeField] Transform view;
    [SerializeField] Animator anim;


    Tween myTween;
    Vector3 lastPos;

    static Vector3 FlattenVector(Vector3 input)
    {
        return new Vector3(input.x, 0, input.z);
    }

    private void Update()
    {
        if(lastPos!=null) {
            previousSpeed = speed;
            speed = Vector3.Distance(FlattenVector(transform.position), lastPos)/Time.deltaTime;
            if (speed > 0 && previousSpeed == 0)
            {
                isMoving = true;
                Skip();
            } else if (speed == 0 && previousSpeed > 0)
            {
                isMoving = false;
                myTween.Kill();
                myTween = null;
            }
        }
        lastPos = FlattenVector(transform.position);
        if(controller!=null)
        {
            Walk();
            Look();
        }

    }

    [SerializeField]
    float skipHeight = .3f;
    [SerializeField]
    float stepLength = .4f;

    public UnityEvent stepLand;
    void Skip()
    {
        if (myTween != null && !myTween.IsComplete()) {
            return;
        }
        anim.SetTrigger("Jump");
        var skipIntensity = Mathf.InverseLerp(0, maxDistance, speed);
        myTween = view.DOLocalMoveY((skipHeight * heightCurve.Evaluate(skipIntensity)), durationCurve.Evaluate(skipIntensity))
            .SetEase(walkPattern)
            .OnComplete(
                () => {
                    stepLand?.Invoke();
                    Skip();
                });
    }
    
    [SerializeField]
    GameObject controller;
    void Walk()
    {
        Vector3 flatTarget = new Vector3(controller.transform.position.x, transform.position.y, controller.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, flatTarget, maxDistance*Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, flatTarget, lerpSpeed * Time.deltaTime);
        
    }

    public void PlayStepSound () {
        //AkSoundEngine.PostEvent(SoundManager.PlayFootstep, gameObject);
    }

    [SerializeField]
    float maxDistance = 1f;
    void Look()
    {

        transform.rotation = controller.transform.rotation;
        return;

        Vector3 flatTarget = new Vector3(controller.transform.position.x, transform.position.y, controller.transform.position.z);

        /*
        Vector3 flatSelf = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 direction = flatTarget - flatSelf;
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
        if (Vector3.Distance(transform.position, controller.transform.position)<.3f)
        {
            //toRotation = Quaternion.FromToRotation(transform.forward, controller.transform.forward);
            transform.LookAt(flatTarget+controller.transform.forward);
        }
        transform.LookAt(flatTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, lerpSpeed *Time.deltaTime);
        
        Quaternion toRotation = Quaternion.FromToRotation(transform.forward, controller.transform.forward);
        */
        transform.LookAt(flatTarget);
    }
}
