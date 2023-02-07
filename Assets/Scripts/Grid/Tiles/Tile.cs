using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour {
    [SerializeField] private bool isInteractable = true;

    [SerializeField]
    public Transform tileTop;

    [SerializeField]
    public GameObject seed;


    [SerializeField]
    public Buildable occupant;

    public UnityEvent onOccupy;
    public UnityEvent onVacate;

    [SerializeField] private GameObject indicator;

    [SerializeField] public MeshRenderer baseMesh, ringMesh;

    [SerializeField] [ColorUsage(true, true)] private Color selectedColor = Color.green;

    [SerializeField] private bool isAreable;

    [SerializeField] private TMP_Text energyDebug;
    public UnityEvent<Tile> onAreate;
    public UnityEvent<Tile> onUnAreate;



    public UnityEvent onSelected;
    public UnityEvent onDeselected;
    private Color borderOriginalCol;
    private bool buildable = false;
    private int energy;


    private List<Tile> neighbors;
    private bool walkable = true;
    public bool IsInteractable => isInteractable;
    public bool IsAreable => isAreable;


    private void OnEnable () {
        neighbors = new List<Tile>();
        foreach (var col in Physics.OverlapSphere(transform.position, transform.localScale.x * 1.3f)) {
            if (col.gameObject == gameObject) continue;
            if (col.TryGetComponent(out Tile tile)) {
                neighbors.Add(tile);
            }
        }

        TileManager.Instance.RegisterTile(this);

        if (ringMesh != null)
            borderOriginalCol = ringMesh.materials[0].color;

    }




    private void OnDrawGizmosSelected () {
        if (neighbors == null) return;
        foreach (var tile in neighbors) {
            Gizmos.color = tile.IsAreable ? Color.green : Color.red;

            Gizmos.DrawWireSphere(tile.transform.position, .2f);
        }
        Gizmos.color = Color.white;
    }

    public void Vacate () {
        occupant = null;
        onVacate?.Invoke();
    }
    public bool Occupy (Buildable newOccupant) {
        if (occupant == null) {
            occupant = newOccupant;
            onOccupy?.Invoke();
            return true;
        }
        return false;
        Debug.LogError("Assigning occupant to occupied tile!");
    }

    public void GrowRoots () {
        if (energy <= 0) return;
        var randoNeighbor = neighbors[Random.Range(0, neighbors.Count)];
        energy--;
        randoNeighbor.MakeAreable(energy);
    }

    public void SpawnSeed () {
        var seedPosition = transform.position + Vector3.up * 0.5f;
        var blah = Instantiate(seed, seedPosition, Quaternion.identity);
        blah.gameObject.SetActive(true);

    }

    public bool IsConnected (Tile targetTile) {
        return true;
        //A* Algorithm here
    }
    public Stack<Tile> GetPath (Tile targetTile) {
        return null;
        //A* pathway here
    }

    private float FTile (Tile targetTile) {
        //for use in A*
        return Vector3.Distance(transform.position, targetTile.transform.position);
    }

    public void Select () {
        //Debug.Log("selecting");
        indicator.SetActive(true);

        var block = new MaterialPropertyBlock();
        ringMesh.GetPropertyBlock(block);//This is some dumb shit cause i dunno what the final mesh will look like
        block.SetColor("_BaseColor", selectedColor);
        ringMesh.SetPropertyBlock(block);

        onSelected?.Invoke();
    }

    public void Deselect () {
        //Debug.Log("deselecting");
        indicator.SetActive(false);

        var block = new MaterialPropertyBlock();
        ringMesh.GetPropertyBlock(block);//This is some dumb shit cause i dunno what the final mesh will look like
        block.SetColor("_BaseColor", borderOriginalCol);
        ringMesh.SetPropertyBlock(block);

        onDeselected?.Invoke();
    }


    public void MakeAreable (int energy) {
        this.energy = energy;
        isAreable = true;
        onAreate?.Invoke(this);


        var block = new MaterialPropertyBlock();
        baseMesh.GetPropertyBlock(block);

        block.SetColor("_BaseColor", Color.green);
        baseMesh.SetPropertyBlock(block);
    }
    public void MakeUnAreable () {
        isAreable = false;
        onUnAreate?.Invoke(this);

        var block = new MaterialPropertyBlock();
        baseMesh.GetPropertyBlock(block);

        block.SetColor("_BaseColor", Color.grey);
        baseMesh.SetPropertyBlock(block);
    }
}