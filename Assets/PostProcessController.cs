using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PostProcessController : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] OxygenTank tank;

    [SerializeField] AnimationCurve damageIntensityCurve;
    ColorAdjustments colAdjust;
    float defaultSaturation;


    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet<ColorAdjustments>(out colAdjust);
        defaultSaturation = colAdjust.saturation.value;

        tank = FindObjectOfType<OxygenTank>();
    }

    private void Update()
    {
        SetSaturation();
    }


    //THIS IS BAD DONT DO THIS AGAIN, BAD PJ
    public void SetSaturation()
    {
        colAdjust.saturation.value = Mathf.Lerp(-100, defaultSaturation, damageIntensityCurve.Evaluate(tank.OxygenPercent));
    }
}
