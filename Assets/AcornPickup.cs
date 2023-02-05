using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornPickup : Pickupable
{
    public override void Pickup(GameObject picker)
    {
        var backpack = picker.GetComponent<Backpack>();
        if (backpack!=null)
        {
            backpack.SetSeeds(1);
            //AkSoundEngine.PostEvent(SoundManager.CollectSoil, gameObject);
        }
        base.Pickup(picker);
        Destroy(gameObject);
    }
    protected virtual void DestroyMe()
    {
    }
}
