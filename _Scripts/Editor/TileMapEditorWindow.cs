///*
// * TileMapEditorWindow.cs
// * Script Author: Charles d'Ansembourg
// * Creation Date: 07/08/2024
// * Contact: c.dansembourg@icloud.com
// */

//using UnityEditor;
//using UnityEngine;

//    public class TileMapEditorWindow : EditorWindow
//    {
//        private TileMapData tileMapData;
//        private int newWidth = 50;
//        private int newHeight = 50;

//        [MenuItem("Window/Tile Map Editor")]
//        public static void ShowWindow()
//        {
//            GetWindow<TileMapEditorWindow>("Tile Map Editor");
//        }

//        private void OnGUI()
//        {
//            GUILayout.Label("Tile Map Editor", EditorStyles.boldLabel);

//            tileMapData = (TileMapData)EditorGUILayout.ObjectField("Tile Map Data", tileMapData, typeof(TileMapData), false);

//            if (tileMapData != null)
//            {
//                newWidth = EditorGUILayout.IntField("Width", newWidth);
//                newHeight = EditorGUILayout.IntField("Height", newHeight);

//                if (GUILayout.Button("Generate Grid"))
//                {
//                    tileMapData.Initialize(newWidth, newHeight);
//                    MarkDirtyAndSave();
//                }

//                EditorGUILayout.Space();

//                if (tileMapData.tiles != null)
//                {
//                    for (int y = tileMapData.height - 1; y >= 0; y--)
//                    {
//                        EditorGUILayout.BeginHorizontal();
//                        for (int x = 0; x < tileMapData.width; x++)
//                        {
//                            int index = y * tileMapData.width + x;
//                            Color originalColor = GUI.backgroundColor;

//                            switch (tileMapData.tiles[index].Type)
//                            {
//                                case TileType.Walkable:
//                                    GUI.backgroundColor = Color.green;
//                                    break;

//                                case TileType.Blocked:
//                                    GUI.backgroundColor = Color.blue;
//                                    break;

//                                case TileType.Wall:
//                                    GUI.backgroundColor = Color.red;
//                                    break;
//                            }

//                            if (GUILayout.Button("", GUILayout.Width(20), GUILayout.Height(20)))
//                            {
//                                CycleTileType(tileMapData.tiles[index]);
//                                MarkDirtyAndSave();
//                            }

//                            GUI.backgroundColor = originalColor;
//                        }
//                        EditorGUILayout.EndHorizontal();
//                    }

//                    EditorGUILayout.Space();

//                    if (GUILayout.Button("Set All Walkable"))
//                    {
//                        SetAllTilesWalkability(TileType.Walkable);
//                        MarkDirtyAndSave();
//                    }

//                    if (GUILayout.Button("Set All Blocked"))
//                    {
//                        SetAllTilesWalkability(TileType.Blocked);
//                        MarkDirtyAndSave();
//                    }

//                    if (GUILayout.Button("Set All Walls"))
//                    {
//                        SetAllTilesWalkability(TileType.Wall);
//                        MarkDirtyAndSave();
//                    }
//                }
//            }
//        }

//        private void CycleTileType(Tile tile)
//        {
//            switch (tile.Type)
//            {
//                case TileType.Walkable:
//                    tile.Type = TileType.Blocked;
//                    break;

//                case TileType.Blocked:
//                    tile.Type = TileType.Wall;
//                    break;

//                case TileType.Wall:
//                    tile.Type = TileType.Walkable;
//                    break;
//            }
//        }

//        private void SetAllTilesWalkability(TileType type)
//        {
//            for (int i = 0; i < tileMapData.tiles.Length; i++)
//            {
//                tileMapData.tiles[i].Type = type;
//            }
//        }

//        private void MarkDirtyAndSave()
//        {
//            EditorUtility.SetDirty(tileMapData);
//            AssetDatabase.SaveAssets();
//        }
//    }