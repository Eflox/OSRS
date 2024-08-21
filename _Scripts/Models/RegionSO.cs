/*
 * RegionSO.cs
 * Script Author: Charles d'Ansembourg
 * Creation Date: 19/08/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace OSRS
{
    [CreateAssetMenu(fileName = "New Region", menuName = "SO", order = 1)]
    public class RegionSO : ScriptableObject
    {
        public GameObject RegionPrefab;
        public Vector2Int Coordinates;
        public Tile[,] Tiles = new Tile[64, 64];
    }
}