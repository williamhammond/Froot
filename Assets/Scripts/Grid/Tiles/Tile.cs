using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;
using TMPro;

public class Tile : MonoBehaviour
{
    bool walkable = true;
    bool buildable = false;

    [SerializeField]
    public Transform tileTop;

    List<Tile> neighbors;


    [SerializeField]
    public Buildable occupant;

    public UnityEvent onOccupy;
    public UnityEvent onVacate;

    [SerializeField]
    GameObject indicator;

    [SerializeField] MeshRenderer baseMesh;
    [SerializeField]
    bool isAreable;

    [SerializeField] TMP_Text energyDebug;
    int energy = 0;
    public bool IsAreable => isAreable;
    public UnityEvent<Tile> onAreate;
    public UnityEvent<Tile> onUnAreate;

    

    public UnityEvent onSelected;
    public UnityEvent onDeselected;

    public void Vacate()
    {
        occupant = null;
        onVacate?.Invoke();
    }
    public bool Occupy(Buildable newOccupant)
    {
        if(occupant == null)
        {
            occupant = newOccupant;
            onOccupy?.Invoke();
            return true;
        }
        else
        {
            return false;
            Debug.LogError("Assigning occupant to occupied tile!");
        }
    }

    public void GrowRoots()
    {
        if (energy <= 0) return;
        var randoNeighbor = neighbors[UnityEngine.Random.Range(0, neighbors.Count)];
        energy--;
        randoNeighbor.MakeAreable(energy);
    }


    private void OnEnable()
    {
        neighbors = new();
        foreach (var col in Physics.OverlapSphere(transform.position, transform.localScale.x * 1.3f))
        {
            if (col.gameObject == gameObject) continue;
            if(col.TryGetComponent<Tile>(out Tile tile))
            {
                neighbors.Add(tile);
            }
        }

        TileManager.Instance.RegisterTile(this);
    }

    public bool IsConnected(Tile targetTile)
    {
        return true;
        //A* Algorithm here
    }
    public Stack<Tile> GetPath(Tile targetTile)
    {
        return null;
        //A* pathway here
    }

    float FTile(Tile targetTile)
    {
        //for use in A*
        return (Vector3.Distance(transform.position, targetTile.transform.position));
    }

    public void Select()
    {
        //Debug.Log("selecting");
        indicator.SetActive(true);
        onSelected?.Invoke();
    }

    public void Deselect()
    {
        //Debug.Log("deselecting");
        indicator.SetActive(false);
        onDeselected?.Invoke();
    }


    public void MakeAreable(int energy)
    {
        this.energy = energy;
        isAreable = true;
        onAreate?.Invoke(this);

        MaterialPropertyBlock block = new();
        baseMesh.GetPropertyBlock(block);
        block.SetColor("_BaseColor", Color.green);
        baseMesh.SetPropertyBlock(block);
    }
    public void MakeUnAreable()
    {
        isAreable = false;
        onUnAreate?.Invoke(this);

        MaterialPropertyBlock block = new();
        baseMesh.GetPropertyBlock(block);
        block.SetColor("_BaseColor", Color.grey);
        baseMesh.SetPropertyBlock(block);
    }




    private void OnDrawGizmosSelected()
    {
        if (neighbors == null) return;
        foreach (var tile in neighbors)
        {
            Gizmos.color = tile.IsAreable ? Color.green : Color.red;

            Gizmos.DrawWireSphere(tile.transform.position, .2f);
        }
        Gizmos.color = Color.white;
    }

}
