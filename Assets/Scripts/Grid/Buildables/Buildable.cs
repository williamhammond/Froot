using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public Tile myTile { get; private set; }
    public int rootEnergy;
    public void Place(Tile targetTile)
    {
        if(targetTile.Occupy(this))
        {
            if(myTile!=null)
            {
                myTile.Vacate();
            }
            myTile = targetTile;
            transform.position = targetTile.tileTop.position;
            targetTile.MakeAreable(rootEnergy);

        }
        else
        {
            Debug.LogWarning("Failed to occupy desired tile!");
        }    
    }

    private void OnDestroy()
    {
        if (myTile!=null)
        {
            myTile.Vacate();
        }
    }
    protected virtual async Task BuildAnimation()
    {
        //Stub for animation
        await Task.CompletedTask;
    }
}
