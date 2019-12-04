using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSlotManager : MonoBehaviour
{
    public SpriteRenderer unitIcon;         // 마우스 따라다니는 유닛 아이콘
    public UnitSlot[] slots;
    public ToggleGroup slotNumberGroup;
    public Toggle[] slotGroupNumbers;

    [HideInInspector]
    public int[] slotGroup;
    [HideInInspector]
    public bool isBuildMode = false;

    public static UnitSlotManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SelectSlot(3);
        if (Input.GetKeyDown(KeyCode.Alpha5))
            SelectSlot(4);
        if (Input.GetKeyDown(KeyCode.Alpha6))
            SelectSlot(5);
    }

    void Start()
    {
        // 데이터매니져에서 유저 슬롯 세팅 가져오기
        slotGroup = DataManager.Instance.userData_setting.GetActivatedSlotGroup();

        // 슬롯 세트 넘버 토글그룹 지정 및 활성화된 그룹넘버 켜기
        for (int i = 0; i < slotGroupNumbers.Length; ++i) slotGroupNumbers[i].group = slotNumberGroup;
        ActivateGroupNumber(DataManager.Instance.userData_setting.GetActivatedSlotGroupIndex());

        // 마지막으로 선택한 슬롯 활성화
        slots[DataManager.Instance.userData_setting.GetSelectedSlot()].SlotOnOff();
        ChangeUnitIcon();

        SetSlots(false);
    }

    // 현재 활성화된 슬롯그룹 정보대로 슬롯ui에 정보 띄우기
    public void SetSlots(bool isBuildMode)
    {
        this.isBuildMode = isBuildMode;

        for (int i = 0; i < slots.Length; ++i)
        {
            if (slotGroup[i] == 0)
            {
                slots[i].SlotInit();
                continue;
            }
            slots[i].ShowSlotContent(isBuildMode, slotGroup[i]);
        }
    }

    public void SetSlots()
    {
        SetSlots(isBuildMode);
    }

    // 슬롯그룹 변경하기(true : 인덱스 1 증가/ false : 인덱스 1 감소)
    public void SlotGroupUpDown(bool upDown)
    {
        int index = DataManager.Instance.userData_setting.GetActivatedSlotGroupIndex();

        if (upDown) index = index + 1 >= UserSettingData.slotGroupCount ? 0 : index + 1;
        if (!upDown) index = index - 1 < 0 ? UserSettingData.slotGroupCount - 1 : index - 1;

        ChangeActivatedSlotGroup(index);
    }

    // 슬롯그룹 변경
    public void ChangeActivatedSlotGroup(int number)
    {
        if (number >= UserSettingData.slotGroupCount) return;

        // 슬롯그룹 인덱스 변경 및 활성화된 슬롯 재할당
        DataManager.Instance.userData_setting.SetActivatedSlotGroupIndex(number);
        slotGroup = DataManager.Instance.userData_setting.GetActivatedSlotGroup();

        // ui 업데이트
        SetSlots(isBuildMode);
        ActivateGroupNumber(number);
    }

    // 슬롯그룹 번호 ui 활성화하는 함수
    public void ActivateGroupNumber(int number)
    {
        slotGroupNumbers[number].isOn = true;
    }

    // 슬롯 선택하는 함수
    public void SelectSlot(int number)
    {
        // 같은 번호를 선택했으면 리턴
        if (number == DataManager.Instance.userData_setting.GetSelectedSlot()) return;

        // 기존에 켜져있던 슬롯 끄기
        slots[DataManager.Instance.userData_setting.GetSelectedSlot()].SlotOnOff();

        // 새 슬롯 인덱스 저장 및 켜기
        DataManager.Instance.userData_setting.SelectSlot(number);
        slots[number].SlotOnOff();

        ChangeUnitIcon();
    }

    // 마우스 따라다니는 유닛 아이콘 이미지 업데이트
    private void ChangeUnitIcon()
    {
        unitIcon.sprite = UnitInfoManager.Instance.unitList[DataManager.Instance.userData_setting.GetSelectedIndex()].unitSprite;
    }
}
