using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEditor;
using System.Linq;

public class HexGrid : MonoBehaviour
{
    [SerializeField]int height = 100;
    [SerializeField]int width = 100;
    [SerializeField]float cellSize = 1;

    [SerializeField] float noiseScale = .5f, offset = 0;
    [SerializeField] AnimationCurve falloff;

    [SerializeField]Tile baseTilePrefab;

    [SerializeField] float cutoff = .05f;

    Dictionary<int2, Tile> TileDict;

    [SerializeField] List<TileThreshold> TileThresholds;
    [System.Serializable]
    public struct TileThreshold
    {
        public Tile prefab;
        public float threshold;
    }

    private void Awake()
    {
        //CreateGrid();
    }


    void DestroyChildren()
    {
        foreach (var tile in GetComponentsInChildren<Tile>())
        {
            DestroyImmediate(tile.gameObject);

            TileDict = new Dictionary<int2, Tile>();
        }
    }

    [ContextMenu("Generate")]
    private void CreateGrid()
    {
        if (TileDict != null)
        {
#if UNITY_EDITOR
            foreach (var tile in TileDict)
            {
                if (tile.Value == null) continue;
                DestroyImmediate(tile.Value.gameObject);
            }
#else
            foreach (var tile in TileDict)
            {
                Destroy(tile.Value.gameObject);
            }
#endif
        }


        TileDict = new Dictionary<int2, Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //Is this an odd row?
                Vector3 pos = new Vector3(x * (cellSize * .5f + cellSize) - cellSize * width *.75f, 0, 0)
                    + new Vector3(0, 0, y * cellSize *1.75f - cellSize * height * .75f)
                    + (x % 2 == 1 ? Vector3.forward * cellSize * 0.866025404f : Vector3.zero);


                //Noise assigned
                var noiseVal = Mathf.PerlinNoise(pos.x * noiseScale + cellSize*height*.75f + offset, pos.z * noiseScale + cellSize * width * .75f + offset);
                noiseVal -= falloff.Evaluate(Vector2.Distance(new Vector2(x , y ), new Vector2(width*.5f, height*.5f))/(width*.5f));

                //Decide what to spawn here
                if (noiseVal > cutoff)
                {
                    var newTile = Instantiate<Tile>(baseTilePrefab);
                    newTile.gameObject.name = $"Tile {x}, {y}";
                    newTile.transform.parent = transform;
                    newTile.transform.position = pos;
                    TileDict.Add(new int2(x, y), newTile);



                    //visualize debug
                    MaterialPropertyBlock block = new MaterialPropertyBlock();
                    newTile.baseMesh.GetPropertyBlock(block);

                    block.SetColor("_BaseColor", Color.white * noiseVal);
                    newTile.baseMesh.SetPropertyBlock(block);
                }



            }
        }
    }
}
