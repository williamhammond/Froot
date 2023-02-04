using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    bool TryBuild(Tile target)
    {
        //Put cost logic here
        return true;
    }

    void Build()
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
