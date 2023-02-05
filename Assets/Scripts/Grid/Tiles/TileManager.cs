using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    public List<Tile> tiles;

    [SerializeField] float tileTickTime = 5f;
    float timer;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        tiles = new();
    }
    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void RegisterTile(Tile tile)
    {
        if (tiles.Contains(tile)) return;

        tiles.Add(tile);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > tileTickTime)
        {
            UpdateTiles();
            timer = 0;
        }
    }

    private void UpdateTiles()
    {
        foreach (var tile in tiles.Where(t => t.IsAreable))
        {
            //Root logic
            tile.GrowRoots();
        }
    }


    [ContextMenu("FetchTiles")]
    public void ParentAllTiles()
    {
        foreach (var t in FindObjectsOfType<Tile>())
        {
            t.transform.parent = transform;
        }
    }
}
