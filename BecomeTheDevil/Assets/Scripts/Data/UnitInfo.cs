using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    [System.Serializable]
    public class DropItem
    {
        public int itemIndex;
        public int itemChange;
    }

    public int unitType = 0;                // 유닛 타입 : 아군(0), 필드몹(1), 웨이브몹(2)
    public string unitName;                 // 유닛 이름
    public int unitIndex = 0;               // 유닛 고유 인덱스(1부터 시작)
    public Sprite unitSprite;               // 유닛 이미지
    public int buildMoney = 0;              // 유닛 생성 시 필요한 돈
    public string buildMaterial;            // 유닛 생성 시 필요한 아이템 이름
    public int dropMoney = 0;               // 죽었을 때 떨어뜨리는 돈 갯수(value 아님)
    public DropItem[] dropItem;                // 죽었을 때 떨어뜨리는 아이템과 확률
}
