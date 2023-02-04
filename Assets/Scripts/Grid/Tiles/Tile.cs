using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    bool walkable = true;
    bool buildable = false;

    [SerializeField]
    public Transform tileTop;

    [SerializeField]
    List<Tile> neighbors;

    [SerializeField]
    public Buildable occupant;

    public UnityEvent onOccupy;
    public UnityEvent onVacate;

    [SerializeField]
    GameObject indicator;

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

    private void OnTriggerEnter(Collider collider)
    {
        //Build tile network
       
        if (neighbors==null)
        {
            neighbors = new List<Tile>();
        }
        var newTile = collider.GetComponent<Tile>();
        if (newTile!=null)
        {
            if(!neighbors.Contains(newTile))
            {
                neighbors.Add(newTile);
            }
        }
    }
    [SerializeField]
    Tile testTile;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("got space");
            if (testTile != null)
            {
                Debug.Log("Searching for tile " + testTile);
                Debug.Log("Found tile? " + IsConnected(testTile));
            }
        }
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
        Debug.Log("selecting");
        indicator.SetActive(true);
        onSelected?.Invoke();
    }

    public void Deselect()
    {
        Debug.Log("deselecting");
        indicator.SetActive(false);
        onDeselected?.Invoke();
    }

}
