using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Targeter : MonoBehaviour
{
    List<Tile> targeted;
    Tile target;
    private void Awake()
    {
        targeted = new List<Tile>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        var hitTile = collision.collider.GetComponentInParent<Tile>();
        if (hitTile!=null)
        {
            if (!targeted.Contains(hitTile))
            {
                targeted.Add(hitTile);
                ResetTarget();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        var hitTile = collision.collider.GetComponentInParent<Tile>();
        if (hitTile != null)
        {
            if (targeted.Contains(hitTile))
            {
                targeted.Remove(hitTile);
                ResetTarget();
            }
        }
    }


    public UnityEvent lostTarget;
    public UnityEvent<Tile> newTarget;
    void ResetTarget()
    {
        if (targeted != null && targeted.Count!=00)
        {
            float closest = 10000;
            Tile tile = null;
            foreach (Tile t in targeted)
            {
                var distance = Vector3.Distance(t.transform.position, transform.position);
                if (distance < closest)
                {
                    closest = distance;
                    tile = t;
                }
            }
            if(tile==null || tile!=target)
            {
                target = tile;
                newTarget?.Invoke(target);
            }
        }
        else
        {
            target = null;
        }
    }
}
