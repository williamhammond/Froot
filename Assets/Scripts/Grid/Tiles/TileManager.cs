using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random=UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;

    public List<Tile> tiles;

    [SerializeField] float tileTickTime = 5f;
    float timer;

    [SerializeField]
    private int seedSpawnPerTick = 1;

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

    private void UpdateTiles() {
        HashSet<int> usedTiles = new HashSet<int>();
        int i = seedSpawnPerTick;
        while (i > 0)
        {
            var tile = Random.Range(0, tiles.Count);
            if (usedTiles.Contains(tile) || !tiles[tile].IsInteractable) {
                continue;
            }
            usedTiles.Add(tile);
            tiles[tile].SpawnSeed();
            i--;
        }
        
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
