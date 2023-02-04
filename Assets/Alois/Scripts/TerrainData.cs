using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class TerrainData : ScriptableObject
{
    public string terrainName;
    public int energy;
    public bool isHope;
    public bool isRock;
    public GameObject hexPrefab;
}
