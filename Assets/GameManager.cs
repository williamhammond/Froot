using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Tile startingTile;
    [SerializeField] Buildable startingBuildablePrefab;
    [SerializeField] int startingEnergy = 8;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void OnDestroy()
    {
        if (Instance != this) return;
        Instance = null;
    }

    private void Start()
    {
        var motherTree = Instantiate(startingBuildablePrefab);
        motherTree.rootEnergy = 0;
        motherTree.Place(startingTile);
        startingTile.MakeAreable(startingEnergy);
        
    }
}
