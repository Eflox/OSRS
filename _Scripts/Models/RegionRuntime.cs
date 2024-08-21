/*
 * RegionRuntimeState.cs
 * Script Author: Charles d'Ansembourg
 * Creation Date: 20/08/2024
 * Contact: c.dansembourg@icloud.com
 */

using System;
using UnityEngine;

namespace OSRS
{
    [Serializable]
    public class RegionRuntimeState
    {
        public RegionSO RegionData;
        public bool Loaded;

        private GameObject _regionObject;

        public RegionRuntimeState(RegionSO regionData)
        {
            RegionData = regionData;
            Loaded = false;
        }

        public void Load()
        {
            Vector3 regionSpawnPosition = new Vector3(64 * RegionData.Coordinates.x, 0, 64 * RegionData.Coordinates.y);
            _regionObject = UnityEngine.Object.Instantiate(RegionData.RegionPrefab, regionSpawnPosition, Quaternion.identity);
            Loaded = true;
        }

        public void Unload()
        {
            UnityEngine.Object.Destroy(_regionObject);
            Loaded = false;
        }
    }
}