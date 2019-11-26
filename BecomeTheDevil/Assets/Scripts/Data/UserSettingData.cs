using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettingData : UserData
{
    public const int slotGroupCount = 5;
    private int[][] slotGroup;              // 유저 슬롯그룹
    private int activatedSlotGroupIndex;    // 활성화된 슬롯그룹 인덱스

    private const string identifier_slotGroup = "identifier_slotSet";
    private const string identifier_activatedSlotGroupIndex = "identifier_activatedSlotSet";

    public UserSettingData()
    {
        InitSlot();

        activatedSlotGroupIndex = 0;

        LoadData();
    }

    public int GetActivatedSlotGroupIndex() { return activatedSlotGroupIndex; }

    public void SetActivatedSlotGroupIndex(int slotIndex)
    {
        activatedSlotGroupIndex = slotIndex;
        SaveData();
    }

    public int[] GetActivatedSlotGroup()
    {
        return GetSlotGroup(activatedSlotGroupIndex);
    }

    public int[] GetSlotGroup(int slotGroupIndex)
    {
        return slotGroup[slotGroupIndex];
    }

    public void SetSlot(int slot, int unitIndex)   // 활성화된 스킬셋에 슬롯 번호와 유닛 인덱스 등록
    {
        slotGroup[activatedSlotGroupIndex][slot] = unitIndex;
        SaveData();
    }

    public void SetSlot(int slotSetIndex, int slot, int unitIndex) 
    {
        slotGroup[slotSetIndex][slot] = unitIndex;
        SaveData();
    }

    public override void SaveData()
    {
        Save<int[][]>(identifier_slotGroup, slotGroup);
        Save<int>(identifier_activatedSlotGroupIndex, activatedSlotGroupIndex);
    }

    public override void LoadData()
    {
        slotGroup = Load<int[][]>(identifier_slotGroup);
        activatedSlotGroupIndex = Load<int>(identifier_activatedSlotGroupIndex);
    }

    public override void DeleteData()
    {
        Delete(identifier_slotGroup);
        Delete(identifier_activatedSlotGroupIndex);

        InitSlot();
        activatedSlotGroupIndex = 0;
    }

    private void InitSlot()
    {
        slotGroup = new int[slotGroupCount][];
        for (int i = 0; i < slotGroup.Length; ++i)
        {
            slotGroup[i] = new int[6];
            for (int j = 0; j < slotGroup[i].Length; ++j) slotGroup[i][j] = 0;
        }

    }
}
