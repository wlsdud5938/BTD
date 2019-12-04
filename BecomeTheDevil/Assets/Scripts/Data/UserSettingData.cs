using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettingData : UserData
{
    public const int slotGroupCount = 5;
    private int[][] slotGroup;              // 유저 슬롯그룹
    private int activatedSlotGroupIndex;    // 활성화된 슬롯그룹 인덱스
    private int selectedSlot;

    private const string identifier_slotGroup = "identifier_slotSet";
    private const string identifier_activatedSlotGroupIndex = "identifier_activatedSlotSet";
    private const string identifier_selectedSlot = "identifier_selectedSlot";

    public UserSettingData()
    {
        InitSlot();

        activatedSlotGroupIndex = 0;
        selectedSlot = 0;

        LoadData();
    }

    public int GetActivatedSlotGroupIndex() { return activatedSlotGroupIndex; }
    public int GetSelectedSlot() { return selectedSlot; }
    
    public int[] GetActivatedSlotGroup()
    {
        return GetSlotGroup(activatedSlotGroupIndex);
    }

    public int[] GetSlotGroup(int slotGroupIndex)
    {
        return slotGroup[slotGroupIndex];
    }
    
    // 현재 활성화된 슬롯그룹의 선택된 슬롯이 저장하는 정보 반환 
    public int GetSelectedIndex()
    {
        return slotGroup[activatedSlotGroupIndex][selectedSlot];
    }

    // 슬롯 선택
    public void SelectSlot(int slot)
    {
        selectedSlot = slot;
        Save<int>(identifier_selectedSlot, selectedSlot);
    }

    // 슬롯 그룹 선택(활성화)
    public void SetActivatedSlotGroupIndex(int slotIndex)
    {
        activatedSlotGroupIndex = slotIndex;
        SaveData();
    }

    // 활성화된 스킬셋에 슬롯 번호와 유닛 인덱스 등록
    public void SetSlot(int slot, int unitIndex)   
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
        Save<int>(identifier_selectedSlot, selectedSlot);
    }

    public override void LoadData()
    {
        slotGroup = Load<int[][]>(identifier_slotGroup);
        activatedSlotGroupIndex = Load<int>(identifier_activatedSlotGroupIndex);
        selectedSlot = Load<int>(identifier_selectedSlot);
    }

    public override void DeleteData()
    {
        Delete(identifier_slotGroup);
        Delete(identifier_activatedSlotGroupIndex);
        Delete(identifier_selectedSlot);

        InitSlot();
        activatedSlotGroupIndex = 0;
        selectedSlot = 0;
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
