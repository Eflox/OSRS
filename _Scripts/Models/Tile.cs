/*
 * Tile.cs
 * Script Author: Charles d'Ansembourg
 * Creation Date: 19/08/2024
 * Contact: c.dansembourg@icloud.com
 */

using System;
using UnityEngine;

namespace OSRS
{
    [Serializable]
    public class Tile
    {
        public Vector2Int Position;
        public Vector3 WorldPosition;
        [SerializeField] public TileType TileType;
        public Vector3[] CornerPoints = new Vector3[4];

        public Tile(Vector3 worldPosition, TileType tileType, Vector2Int position)
        {
            WorldPosition = worldPosition;
            TileType = tileType;
            CornerPoints.GetCorners(WorldPosition);
            Position = position;
        }
    }
}