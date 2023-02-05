using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPickup : Pickupable
{
    // Start is called before the first frame update
    public override void Pickup(GameObject picker)
    {
        //Debug.Log("LF  Tank");
        var tank = picker.transform.parent.GetComponentInChildren<OxygenTank>();
        if (tank != null)
        {
           // Debug.Log("Found tank");
            tank.FillMax();
            //AkSoundEngine.PostEvent(SoundManager.CollectSoil, gameObject);
        }
        base.Pickup(picker);
        Destroy(gameObject);
    }
    protected virtual void DestroyMe()
    {
    }
}
