using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWalk : MonoBehaviour
{
    [SerializeField]
    AnimationCurve walkPattern;
    [SerializeField]
    float threshold = .3f;
    [SerializeField]
    float speed = 0f;
    

    Tween myTween;
    Vector3 lastPos;
    private void Update()
    {
        if(lastPos!=null)
        {
            speed = Vector3.Distance(transform.position, lastPos)/Time.deltaTime;
        }
        lastPos = transform.position;
        if(controller!=null)
        {
            Walk();
            Look();
        }
        if(myTween==null||myTween.IsComplete())
        {
            StartSkip();
        }
        else if(myTween.IsPlaying())
        {
            if(speed<threshold)
            {
                myTween.Complete();
            }
        }
    }

    [SerializeField]
    float skipHeight = .3f;
    [SerializeField]
    float stepLength = .4f;

    public UnityEvent stepLand;
    void StartSkip()
    {
        Debug.Log("Speed check: " + speed);
        if(myTween!=null)
        { myTween.Kill(); }
        if(speed>=threshold)
        {
            Debug.Log("Initiating walk");
            var newLength = stepLength * (speed / threshold);
            myTween = transform.DOMoveY(skipHeight, newLength);
            myTween.SetEase(walkPattern);
            myTween.Play();
            myTween.OnComplete(() => stepLand?.Invoke()) ;
        }
    }
    [SerializeField]
    GameObject controller;
    void Walk()
    {
        Vector3 flatTarget = new Vector3(controller.transform.position.x, transform.position.y, controller.transform.position.z);

        transform.position = Vector3.Lerp(transform.position, flatTarget, lerpSpeed * Time.deltaTime);
        
    }

    [SerializeField]
    float lerpSpeed = 1f;
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
