using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWalk : MonoBehaviour
{
    [SerializeField]
    AnimationCurve walkPattern, heightCurve;
    [SerializeField]
    float speed = 0f;
    

    Tween myTween;
    Vector3 lastPos;

    private void Start()
    {
        StartSkip();
    }
    static Vector3 FlattenVector(Vector3 input)
    {
        return new Vector3(input.x, 0, input.z);
    }
    private void Update()
    {
        if(lastPos!=null)
        {
            speed = Vector3.Distance(FlattenVector(transform.position), lastPos)/Time.deltaTime;
            Debug.Log("My speed: " + speed);
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
    void StartSkip()
    {
        if(myTween!=null)
        { myTween.Complete(); }
        if(true)//speed>=threshold)
        {
            var skipIntensity = Mathf.InverseLerp(0, maxDistance, speed);
            Debug.Log("Skip intensity: " + skipIntensity);
            var duration = skipIntensity;
            if (duration < .2)
            {
                duration = .2f;
            }
            Debug.Log("Initiating skip: " + duration);
            myTween = transform.DOMoveY(controller.transform.position.y + (skipHeight * heightCurve.Evaluate(skipIntensity)), duration);
            myTween.SetEase(walkPattern);
            myTween.Play();
            myTween.OnComplete(() => stepLand?.Invoke()) ;
            myTween.OnComplete(() => StartSkip());
            //myTween.SetSpeedBased();
        }
    }
    [SerializeField]
    GameObject controller;
    void Walk()
    {
        Vector3 flatTarget = new Vector3(controller.transform.position.x, transform.position.y, controller.transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, flatTarget, maxDistance*Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, flatTarget, lerpSpeed * Time.deltaTime);
        
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
