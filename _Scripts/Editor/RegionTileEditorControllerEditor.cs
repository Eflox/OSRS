///*
// * RegionTileEditorControllerEditor.cs
// * Script Author: Charles d'Ansembourg
// * Creation Date: 19/08/2024
// * Contact: c.dansembourg@icloud.com
// */

//using OSRS;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(RegionTileEditorController))]
//public class RegionTileEditorControllerEditor : Editor
//{
//    private RegionTileEditorController _controller;
//    [SerializeField] private RegionSO _regionSO;

//    private void OnEnable()
//    {
//        _controller = (RegionTileEditorController)target;
//    }

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        GUILayout.Space(10);

//        // Tile Type Buttons
//        if (GUILayout.Button("Load Region"))
//        {
//            _controller.LoadRegion();
//        }

//        if (GUILayout.Button("Clear Region"))
//        {
//            _controller.ClearRegion();
//        }

//        if (GUILayout.Button("Generate Tiles"))
//        {
//            _controller.GenerateTiles();
//        }

//        GUILayout.Label("Select Tile Type", EditorStyles.boldLabel);

//        if (GUILayout.Button("Walkable Tile"))
//        {
//            _controller.SetCurrentTileType(TileType.Walkable);
//        }

//        if (GUILayout.Button("Blocked Tile"))
//        {
//            _controller.SetCurrentTileType(TileType.Blocked);
//        }

//        if (GUILayout.Button("Wall Tile"))
//        {
//            _controller.SetCurrentTileType(TileType.Wall);
//        }

//        GUILayout.Space(10);

//        // Tool Type Buttons
//        GUILayout.Label("Select Tool", EditorStyles.boldLabel);

//        if (GUILayout.Button("Bucket Tool"))
//        {
//            _controller.SetCurrentToolType(ToolType.Bucket);
//        }

//        if (GUILayout.Button("Brush Tool"))
//        {
//            _controller.SetCurrentToolType(ToolType.Brush);
//        }

//        GUILayout.Space(10);

//        // Save Button
//        if (GUILayout.Button("Save Region"))
//        {
//            _controller.SaveRegion();
//        }
//    }

//    // Handles mouse input in the Scene view
//    private void OnSceneGUI()
//    {
//        HandleMouseInput();
//    }

//    private void HandleMouseInput()
//    {
//        Event e = Event.current;

//        if (e.type == EventType.MouseDown && e.button == 0)
//        {
//            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
//            if (Physics.Raycast(ray, out RaycastHit hit))
//            {
//                Tile tile = hit.point.GetTile(_controller.Region.Tiles);
//                if (tile != null)
//                {
//                    _controller.HandleTileChange(tile);
//                }
//            }

//            e.Use(); // Mark event as used
//        }
//    }
//}