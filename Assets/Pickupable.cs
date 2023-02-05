using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField]
    GameObject pickupAnimation;
    public virtual void Pickup(GameObject picker)
    {
        if(pickupAnimation!=null)
        {
            var anim = Instantiate(pickupAnimation);
            anim.transform.position = transform.position;
        }
        DestroyMe();
    }
    protected virtual void DestroyMe()
    {
        Destroy(gameObject);
    }
}
