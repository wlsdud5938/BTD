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
    [CreateAssetMenu(fileName = "New Corner Tile", menuName = "Tiles/Corner Tile")]
    public class CornerTile : TileBase
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

            bool[] masks = { false, false, false, false, false, false, false, false, false, false };    // 각 방향에 타일이 연결되어 있는가
            int count = 0;  // 바닥의 수
            masks[1] = TileValue(tileMap, location + new Vector3Int(-1, 1, 0));    // 1번
            masks[2] = TileValue(tileMap, location + new Vector3Int(0, 1, 0));    // 2번
            masks[3] = TileValue(tileMap, location + new Vector3Int(1, 1, 0));   // 3번
            masks[4] = TileValue(tileMap, location + new Vector3Int(-1, 0, 0));     // 4번
            masks[6] = TileValue(tileMap, location + new Vector3Int(1, 0, 0));    // 6번
            masks[7] = TileValue(tileMap, location + new Vector3Int(-1, -1, 0));     // 7번
            masks[8] = TileValue(tileMap, location + new Vector3Int(0, -1, 0));     // 8번
            masks[9] = TileValue(tileMap, location + new Vector3Int(1, -1, 0));    // 9번

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
            if (masks[4])
            {
                if (masks[2]) return 5;
                if (masks[8]) return 4;
                return 1;
            }
            if (masks[6])
            {
                if (masks[2]) return 6;
                if (masks[8]) return 3;
                return 1;
            }
            if (masks[2])
            {
                if (masks[4]) return 5;
                if (masks[6]) return 6;
                return 2;
            }
            if (masks[8])
            {
                if (masks[4]) return 4;
                if (masks[6]) return 3;
                return 2;
            }

            return 0;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CornerTile))]
    public class CornerTileEditor : Editor
    {
        private CornerTile tile { get { return (target as CornerTile); } }

        public void OnEnable()
        {
            if (tile.m_Sprites == null || tile.m_Sprites.Length != 7)
            {
                tile.m_Sprites = new Sprite[7];
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
            tile.m_Sprites[0] = (Sprite)EditorGUILayout.ObjectField("Icon", tile.m_Sprites[0], typeof(Sprite), false, null);
            tile.m_Sprites[1] = (Sprite)EditorGUILayout.ObjectField("가로 연결", tile.m_Sprites[1], typeof(Sprite), false, null);
            tile.m_Sprites[2] = (Sprite)EditorGUILayout.ObjectField("세로 연결", tile.m_Sprites[2], typeof(Sprite), false, null);
            tile.m_Sprites[3] = (Sprite)EditorGUILayout.ObjectField("하-우", tile.m_Sprites[3], typeof(Sprite), false, null);
            tile.m_Sprites[4] = (Sprite)EditorGUILayout.ObjectField("하-좌", tile.m_Sprites[4], typeof(Sprite), false, null);
            tile.m_Sprites[5] = (Sprite)EditorGUILayout.ObjectField("상-좌", tile.m_Sprites[5], typeof(Sprite), false, null);
            tile.m_Sprites[6] = (Sprite)EditorGUILayout.ObjectField("상-우", tile.m_Sprites[6], typeof(Sprite), false, null);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);

            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }
#endif
}