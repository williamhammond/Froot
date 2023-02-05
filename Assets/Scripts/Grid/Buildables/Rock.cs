using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public List<GameObject> rockMeshes;



    public void Init()
    {
        Instantiate(rockMeshes[Random.Range(0, rockMeshes.Count)],transform);
    }

    
}
