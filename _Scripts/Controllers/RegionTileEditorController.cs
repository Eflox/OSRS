/*
 * RegionTileEditorController.cs
 * Script Author: Charles d'Ansembourg
 * Creation Date: 19/08/2024
 * Contact: c.dansembourg@icloud.com
 */

using UnityEditor;
using UnityEngine;

namespace OSRS
{
    [ExecuteInEditMode]
    public class RegionTileEditorController : MonoBehaviour
    {
        [SerializeField] private RegionSO _region;
        private RegionSO _regionToEdit;
        public RegionSO Region => _regionToEdit;

        [SerializeField] private bool _drawGizmos;
        private TileType _currentTileType;
        private ToolType _currentToolType;

        private GameObject _loadedRegionObject;

        private void OnEnable()
        {
            ClearRegion();
        }

        public void LoadRegion()
        {
            if (_region == null)
                return;

            ClearRegion();

            _regionToEdit = _region;

            if (_loadedRegionObject != null)
                DestroyImmediate(_loadedRegionObject);

            _loadedRegionObject = Instantiate(_regionToEdit.RegionPrefab, Vector3.zero, Quaternion.identity);

            if (_regionToEdit.Tiles.Length < 64)
                GenerateTiles();
        }

        public void ClearRegion()
        {
            if (_loadedRegionObject != null)
                DestroyImmediate(_loadedRegionObject);

            _regionToEdit = null;
        }

        public void GenerateTiles()
        {
            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    Vector3 worldPosition = new Vector3(x, 0, y).GetHeight();
                    _regionToEdit.Tiles[x, y] = new Tile(worldPosition, TileType.Walkable, new Vector2Int(x, y));
                }
            }
        }

        public void SetCurrentTileType(TileType tileType)
        {
            _currentTileType = tileType;
        }

        public void SetCurrentToolType(ToolType toolType)
        {
            _currentToolType = toolType;
        }

        public void HandleTileChange(Tile tile)
        {
            switch (_currentToolType)
            {
                case ToolType.Bucket:
                    BucketTool(tile);
                    break;

                case ToolType.Brush:
                    tile.TileType = _currentTileType;
                    break;
            }
        }

        public void SaveRegion()
        {
            EditorUtility.SetDirty(_regionToEdit); // Mark as dirty
            AssetDatabase.SaveAssets(); // Save changes to disk
            AssetDatabase.Refresh(); // Optional: Refresh the asset database to see changes
        }

        private void BucketTool(Tile startTile)
        {
            TileType targetType = startTile.TileType;

            if (targetType == _currentTileType)
                return;

            FloodFill(startTile, targetType);
        }

        private void FloodFill(Tile tile, TileType targetType)
        {
            if (tile == null || tile.TileType != targetType)
                return;

            tile.TileType = _currentTileType;

            if (tile.Position.x > 0)
                FloodFill(_regionToEdit.Tiles[tile.Position.x - 1, tile.Position.y], targetType);
            if (tile.Position.x < 63)
                FloodFill(_regionToEdit.Tiles[tile.Position.x + 1, tile.Position.y], targetType);
            if (tile.Position.y > 0)
                FloodFill(_regionToEdit.Tiles[tile.Position.x, tile.Position.y - 1], targetType);
            if (tile.Position.y < 63)
                FloodFill(_regionToEdit.Tiles[tile.Position.x, tile.Position.y + 1], targetType);
        }

        public void OnDrawGizmos()
        {
            if (_regionToEdit == null) return;

            if (!_drawGizmos || _regionToEdit.Tiles == null) return;

            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    Tile tile = _regionToEdit.Tiles[x, y];

                    if (tile != null)
                    {
                        Gizmos.color = tile.TileType switch
                        {
                            TileType.Walkable => new Color(0, 1, 0, 1f),
                            TileType.Blocked => new Color(0, 0, 1, 1f),
                            TileType.Wall => new Color(1, 0, 0, 1f),
                            _ => new Color(0, 0, 0, 0)
                        };

                        Gizmos.DrawLineStrip(tile.CornerPoints, true);
                    }
                }
            }
        }

        // Custom Inspector GUI in the Editor
        [CustomEditor(typeof(RegionTileEditorController))]
        public class RegionTileEditorControllerInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                RegionTileEditorController controller = (RegionTileEditorController)target;

                DrawDefaultInspector();

                GUILayout.Space(10);

                // Tile Type Buttons
                if (GUILayout.Button("Load Region"))
                {
                    controller.LoadRegion();
                }

                if (GUILayout.Button("Clear Region"))
                {
                    controller.ClearRegion();
                }

                if (GUILayout.Button("Generate Tiles"))
                {
                    controller.GenerateTiles();
                }

                GUILayout.Label("Select Tile Type", EditorStyles.boldLabel);

                if (GUILayout.Button("Walkable Tile"))
                {
                    controller.SetCurrentTileType(TileType.Walkable);
                }

                if (GUILayout.Button("Blocked Tile"))
                {
                    controller.SetCurrentTileType(TileType.Blocked);
                }

                if (GUILayout.Button("Wall Tile"))
                {
                    controller.SetCurrentTileType(TileType.Wall);
                }

                GUILayout.Space(10);

                // Tool Type Buttons
                GUILayout.Label("Select Tool", EditorStyles.boldLabel);

                if (GUILayout.Button("Bucket Tool"))
                {
                    controller.SetCurrentToolType(ToolType.Bucket);
                }

                if (GUILayout.Button("Brush Tool"))
                {
                    controller.SetCurrentToolType(ToolType.Brush);
                }

                GUILayout.Space(10);

                // Save Button
                if (GUILayout.Button("Save Region"))
                {
                    controller.SaveRegion();
                }
            }

            // Handles mouse input in the Scene view
            private void OnSceneGUI()
            {
                RegionTileEditorController controller = (RegionTileEditorController)target;
                HandleMouseInput(controller);
            }

            private void HandleMouseInput(RegionTileEditorController controller)
            {
                Event e = Event.current;

                if (e.type == EventType.MouseDown && e.button == 0)
                {
                    Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        Tile tile = hit.point.GetTile(controller.Region.Tiles);
                        if (tile != null)
                        {
                            controller.HandleTileChange(tile);
                        }
                    }

                    e.Use(); // Mark event as used
                }
            }
        }
    }
}