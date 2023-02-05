using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TankUI : MonoBehaviour
{
    [SerializeField]
    Gradient colorGradient;
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    GameObject airLevel;

    Vector3 startScale;
    private void Awake()
    {
        startScale = airLevel.transform.localScale;
    }
    [SerializeField]
    bool scale = true;
    public void UpdateTank(int newLevel, int maxLevel)
    {
        float percent = (float)newLevel / (float)maxLevel;
        //Debug.Log("Tank % " + percent);
       if(scale) airLevel.transform.localScale = new Vector3(startScale.x,(percent) * startScale.y, startScale.z);
        if(text!=null)
        {
            text.text = newLevel.ToString();
        }
        var newColor = colorGradient.Evaluate(percent);
        ChangeColor(newColor);
    }

    [SerializeField]
    float intensity = 0.6299262f;
    void ChangeColor(Color newColor)
    {
        var mat = airLevel.GetComponent<MeshRenderer>().material;
        mat.color = newColor;
        //newColor.a = mat.GetColor("_EmissionColor").a;
        mat.SetColor("_EmissionColor", newColor * intensity);
    }
    Tween myTween;
    [SerializeField]
    GameObject shakeable;
    public void Pulse()
    {
        if (myTween != null && myTween.IsPlaying())
        {
            return;
        }
        else
        {
            myTween = shakeable.transform.DOShakeScale(1, .6f, 8, 45);
            myTween.SetEase(Ease.InOutSine);
            myTween.Play();
        }
    }

}
