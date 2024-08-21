/*
 * PlayerMovementController.cs
 * Script Author: Charles d'Ansembourg
 * Creation Date: 20/08/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEngine;

namespace OSRS
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Vector2Int _regionCoordinates;
        [SerializeField] private Vector2Int _playerCoordinates;

        [SerializeField] private GameObject _playerObject;

        private RegionController _mapController;

        private Vector2Int _oldRegionCoordinates;

        private void Start()
        {
            _mapController = FindObjectOfType<RegionController>();
            _mapController.PlayerEnterNewRegion(_regionCoordinates);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
                _playerCoordinates.y++;
            if (Input.GetKey(KeyCode.DownArrow))
                _playerCoordinates.y--;
            if (Input.GetKey(KeyCode.LeftArrow))
                _playerCoordinates.x--;
            if (Input.GetKey(KeyCode.RightArrow))
                _playerCoordinates.x++;

            Vector3 position = new Vector3((_regionCoordinates.x * 64) + _playerCoordinates.x, 0, (_regionCoordinates.y * 64) + _playerCoordinates.y);
            _playerObject.transform.position = position;

            if (_playerCoordinates.y >= 64)
            {
                _playerCoordinates.y = 0;
                _regionCoordinates.y++;
            }
            if (_playerCoordinates.y < 0)
            {
                _playerCoordinates.y = 63;
                _regionCoordinates.y--;
            }
            if (_playerCoordinates.x >= 64)
            {
                _playerCoordinates.x = 0;
                _regionCoordinates.x++;
            }
            if (_playerCoordinates.x < 0)
            {
                _playerCoordinates.x = 63;
                _regionCoordinates.x--;
            }

            if (_oldRegionCoordinates != _regionCoordinates)
            {
                _mapController.PlayerEnterNewRegion(_regionCoordinates);
            }

            _oldRegionCoordinates = _regionCoordinates;
        }
    }
}