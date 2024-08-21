/*
 * Vector3Extensions.cs
 * Script Author: Charles d'Ansembourg
 * Creation Date: 06/08/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace OSRS
{
    public static class Vector3Extensions
    {
        public static Vector3 Round(this Vector3 vector)
        {
            return new Vector3(
                Mathf.Round(vector.x),
                vector.y,
                Mathf.Round(vector.z)
            );
        }

        public static Vector3 GetHeight(this Vector3 vector)
        {
            Ray ray = new Ray(new Vector3(vector.x, 10f, vector.z), Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 20f))
                return new Vector3(vector.x, hit.point.y, vector.z);

            return vector;
        }

        public static Vector3[] GetCorners(this Vector3[] corners, Vector3 position)
        {
            corners[0] = (position + new Vector3(-0.5f, 0, 0.5f)).GetHeight();
            corners[1] = (position + new Vector3(0.5f, 0, 0.5f)).GetHeight();
            corners[2] = (position + new Vector3(0.5f, 0, -0.5f)).GetHeight();
            corners[3] = (position + new Vector3(-0.5f, 0, -0.5f)).GetHeight();

            return corners;
        }

        public static Tile GetTile(this Vector3 position, Tile[,] tiles)
        {
            var roundedPosition = position.Round();

            int x = (int)roundedPosition.x;
            int y = (int)roundedPosition.z;

            return tiles[x, y];
        }
    }
}