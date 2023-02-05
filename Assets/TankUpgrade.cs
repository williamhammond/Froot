using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankUpgrade : Pickupable
{
    // Start is called before the first frame update

    [SerializeField]
    int tankSize;
    public override void Pickup(GameObject picker)
    {
        //Debug.Log("LF  Tank");
        var tank = picker.transform.parent.GetComponentInChildren<OxygenTank>();
        if (tank != null)
        {
            //Debug.Log("Found tank");
            tank.UpdateMax(tankSize);
            AkSoundEngine.PostEvent(SoundManager.CollectSoil, gameObject);
        }
        base.Pickup(picker);
        Destroy(gameObject);
    }
    protected virtual void DestroyMe()
    {
    }
}
