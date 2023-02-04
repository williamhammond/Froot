using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Targeter : MonoBehaviour
{
    List<Tile> targeted;
    
    public Tile target;
    private void Awake()
    {
        targeted = new List<Tile>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("new trigger hit");
        var hitTile = collider.GetComponent<Tile>();
        if (hitTile!=null)
        {
            Debug.Log("hit a tile");
            if (!targeted.Contains(hitTile))
            {
                Debug.Log("new tile");
                targeted.Add(hitTile);
                ResetTarget();
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        var hitTile = collider.GetComponent<Tile>();
        if (hitTile != null)
        {
            if (targeted.Contains(hitTile))
            {
                targeted.Remove(hitTile);
                hitTile.Deselect();
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
                if(target!=null)
                {
                    target.Deselect();
                }
                target = tile;
                target.Select();
                newTarget?.Invoke(target);
            }
        }
        else
        {
            target = null;
        }
    }
}
