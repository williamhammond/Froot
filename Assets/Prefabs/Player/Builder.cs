using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Builder : MonoBehaviour
{
    bool TryBuild(Tile target)
    {
        //Put cost logic here
        if (target.occupant == null) 
        { 
            return true;
        }
        return false;
    }

    private void Update()
    {
        if(Keyboard.current[Key.Space].wasPressedThisFrame)
        {
            Debug.Log("Hit space!");
            Build();
        }
    }
    public void Build()
    {
        Tile targetTile = gameObject.GetComponentInChildren<Targeter>().target;
        Build(targetTile, buildable);
    }

    [SerializeField]
    Buildable buildable;
    void Build(Tile target, Buildable buildable)
    {
        if(TryBuild(target))
        {
            var newBuild = GameObject.Instantiate(buildable);
            newBuild.Place(target);
        }
    }
}
