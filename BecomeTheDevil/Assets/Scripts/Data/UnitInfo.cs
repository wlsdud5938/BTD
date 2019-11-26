using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public string unitName;                 // 유닛 이름
    public int unitIndex = 0;               // 유닛 고유 인덱스(1부터 시작)
    public Sprite unitSprite;               // 유닛 이미지
    public int buildMoney = 0;              // 유닛 생성 시 필요한 돈
    public string buildMaterial;            // 유닛 생성 시 필요한 아이템 이름
}
