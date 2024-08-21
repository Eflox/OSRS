/*
 * RegionController.cs
 * Script Author: Charles d'Ansembourg
 * Creation Date: 20/08/2024
 * Contact: c.dansembourg@icloud.com
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OSRS
{
    public class RegionController : MonoBehaviour
    {
        [SerializeField] private List<RegionSO> _regions;
        [SerializeField] private List<RegionRuntimeState> _runtimeRegions;

        private void Awake()
        {
            _regions = new List<RegionSO>(Resources.LoadAll<RegionSO>("Regions"));
            _runtimeRegions = _regions.Select(r => new RegionRuntimeState(r)).ToList();
        }

        private void Start()
        {
            foreach (var runtimeRegion in _runtimeRegions)
                runtimeRegion.Loaded = false;
        }

        public void PlayerEnterNewRegion(Vector2Int regionPosition)
        {
            var positionsToLoad = (from x in Enumerable.Range(-1, 3)
                                   from y in Enumerable.Range(-1, 3)
                                   select new Vector2Int(x, y) + regionPosition).ToList();

            var regionsToLoad = _runtimeRegions.Where(r => positionsToLoad.Contains(r.RegionData.Coordinates)).ToList();
            var regionsToUnload = _runtimeRegions.Where(r => r.Loaded && !positionsToLoad.Contains(r.RegionData.Coordinates)).ToList();

            foreach (var runtimeRegion in regionsToUnload)
                runtimeRegion.Unload();

            foreach (var runtimeRegion in regionsToLoad)
                if (!runtimeRegion.Loaded)
                    runtimeRegion.Load();
        }

        private void OnDrawGizmos()
        {
            int rows = 4;
            int columns = 5;
            float cellSize = 64;

            Gizmos.color = Color.green;

            for (int x = 0; x <= columns; x++)
            {
                Vector3 start = new Vector3(x * cellSize, 0, 0);
                Vector3 end = new Vector3(x * cellSize, 0, rows * cellSize);
                Gizmos.DrawLine(start, end);
            }

            for (int y = 0; y <= rows; y++)
            {
                Vector3 start = new Vector3(0, 0, y * cellSize);
                Vector3 end = new Vector3(columns * cellSize, 0, y * cellSize);
                Gizmos.DrawLine(start, end);
            }
        }
    }
}