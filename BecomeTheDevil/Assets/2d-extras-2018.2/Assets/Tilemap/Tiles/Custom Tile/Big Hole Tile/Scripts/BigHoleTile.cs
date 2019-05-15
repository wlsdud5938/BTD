using System;
using System.Collections;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;


namespace UnityEngine.Tilemaps
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Big Hole Tile", menuName = "Tiles/Big Hole Tile")]
    public class BigHoleTile : TileBase
    {
        [SerializeField]
        public Sprite[] m_Sprites;

        public override void RefreshTile(Vector3Int location, ITilemap tileMap)
        {
            for (int yd = -1; yd <= 1; yd++)
                for (int xd = -1; xd <= 1; xd++)
                {
                    Vector3Int position = new Vector3Int(location.x + xd, location.y + yd, location.z);
                    if (TileValue(tileMap, position))
                        tileMap.RefreshTile(position);
                }
        }

        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            UpdateTile(location, tileMap, ref tileData);
        }

        private void UpdateTile(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.identity;
            tileData.color = Color.white;

            bool[] masks = {false, true, true, true, true, false, true, true, true, true};    // 각 방향에 바닥이 존재하는가
            int count = 0;  // 바닥의 수
            masks[1] = !TileValue(tileMap, location + new Vector3Int(-1, 1, 0));    // 1번
            masks[2] = !TileValue(tileMap, location + new Vector3Int(0, 1, 0));    // 2번
            masks[3] = !TileValue(tileMap, location + new Vector3Int(1, 1, 0));   // 3번
            masks[4] = !TileValue(tileMap, location + new Vector3Int(-1, 0, 0));     // 4번
            masks[6] = !TileValue(tileMap, location + new Vector3Int(1, 0, 0));    // 6번
            masks[7] = !TileValue(tileMap, location + new Vector3Int(-1, -1, 0));     // 7번
            masks[8] = !TileValue(tileMap, location + new Vector3Int(0, -1, 0));     // 8번
            masks[9] = !TileValue(tileMap, location + new Vector3Int(1, -1, 0));    // 9번

            for (int i = 0; i < masks.Length; ++i) if (masks[i]) count += 1;
           
            int index = GetIndex(masks, count);
            if (index >= 0 && index < m_Sprites.Length && TileValue(tileMap, location))
            {
                tileData.sprite = m_Sprites[index];
                tileData.transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 0f), Vector3.one);
                tileData.color = Color.white;
                tileData.flags = TileFlags.LockTransform | TileFlags.LockColor;
                tileData.colliderType = Tile.ColliderType.Sprite;
            }
        }

        private bool TileValue(ITilemap tileMap, Vector3Int position)
        {
            TileBase tile = tileMap.GetTile(position);
            return (tile != null && tile == this);
        }

        private int GetIndex(bool[] masks, int count)
        {
            if (count == 1)  for (int i = 0; i < masks.Length; ++i) if (masks[i]) return i;
            if (masks[2])
            {
                if (masks[4]) return 10;
                if (masks[6]) return 11;
                return 2;
            }
            if (masks[4])
            {
                if (masks[2]) return 10;
                if (masks[8]) return 12;
                return 4;
            }
            if (masks[6])
            {
                if (masks[2]) return 11;
                if (masks[8]) return 13;
                return 6;
            }
            if (masks[8])
            {
                if (masks[4]) return 12;
                if (masks[6]) return 13;
                return 8;
            }
            if (count == 0) return 14;
            return 5;
        }                
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BigHoleTile))]
    public class BigHoleTileEditor : Editor
    {
        private BigHoleTile tile { get { return (target as BigHoleTile); } }

        public void OnEnable()
        {
            if (tile.m_Sprites == null || tile.m_Sprites.Length != 15)
            {
                tile.m_Sprites = new Sprite[15];
                EditorUtility.SetDirty(tile);
            }
        }


        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Place sprites shown based on the contents of the sprite.");
            EditorGUILayout.Space();

            float oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 210;

            EditorGUI.BeginChangeCheck();
            tile.m_Sprites[0] = (Sprite)EditorGUILayout.ObjectField("Filled (Icon)", tile.m_Sprites[0], typeof(Sprite), false, null);
            tile.m_Sprites[1] = (Sprite)EditorGUILayout.ObjectField("1", tile.m_Sprites[1], typeof(Sprite), false, null);
            tile.m_Sprites[2] = (Sprite)EditorGUILayout.ObjectField("2", tile.m_Sprites[2], typeof(Sprite), false, null);
            tile.m_Sprites[3] = (Sprite)EditorGUILayout.ObjectField("3", tile.m_Sprites[3], typeof(Sprite), false, null);
            tile.m_Sprites[4] = (Sprite)EditorGUILayout.ObjectField("4", tile.m_Sprites[4], typeof(Sprite), false, null);
            tile.m_Sprites[5] = (Sprite)EditorGUILayout.ObjectField("5 (empty)", tile.m_Sprites[5], typeof(Sprite), false, null);
            tile.m_Sprites[6] = (Sprite)EditorGUILayout.ObjectField("6", tile.m_Sprites[6], typeof(Sprite), false, null);
            tile.m_Sprites[7] = (Sprite)EditorGUILayout.ObjectField("7", tile.m_Sprites[7], typeof(Sprite), false, null);
            tile.m_Sprites[8] = (Sprite)EditorGUILayout.ObjectField("8", tile.m_Sprites[8], typeof(Sprite), false, null);
            tile.m_Sprites[9] = (Sprite)EditorGUILayout.ObjectField("9", tile.m_Sprites[9], typeof(Sprite), false, null);
            tile.m_Sprites[10] = (Sprite)EditorGUILayout.ObjectField("2,4", tile.m_Sprites[10], typeof(Sprite), false, null);
            tile.m_Sprites[11] = (Sprite)EditorGUILayout.ObjectField("2,6", tile.m_Sprites[11], typeof(Sprite), false, null);
            tile.m_Sprites[12] = (Sprite)EditorGUILayout.ObjectField("4,8", tile.m_Sprites[12], typeof(Sprite), false, null);
            tile.m_Sprites[13] = (Sprite)EditorGUILayout.ObjectField("6,8", tile.m_Sprites[13], typeof(Sprite), false, null);
            tile.m_Sprites[14] = (Sprite)EditorGUILayout.ObjectField("All Blocked", tile.m_Sprites[14], typeof(Sprite), false, null);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);

            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }
#endif
}