using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class AlternateTile : Tile
{
    [SerializeField] private Sprite[] tiles;
    
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        uint mask = (uint) (position.x);
        tileData.sprite = tiles[mask % 3];
        tileData.colliderType = Tile.ColliderType.Sprite;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Tiles/Alternating Tile")]
    public static void CreateAlternatingTile()
    {
        
        string path = EditorUtility.SaveFilePanelInProject(
            "Alternating Tile",
            "New Alternating Tile",
            "Asset",
            "Please enter a name for the new alternating tile",
            "Assets");

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<AlternateTile>(), path);
    }
#endif
}