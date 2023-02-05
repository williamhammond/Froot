using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    public List<GameObject> rockMeshes;

    private void Start()
    {

        if(transform.parent.TryGetComponent<Tile>(out Tile t))
        {
            GetComponent<Buildable>().Place(t);
        }
    }

    public void Init()
    {
        Instantiate(rockMeshes[Random.Range(0, rockMeshes.Count)],transform);
    }

    
}
