using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 모든 데이터에 접근, 관리하는 클래스
    // 유저 데이터 : 보유한 아이템, 플레이 세팅 등 유저 개별의 정보를 담고있는 데이터들입니다
    // 게임 데이터 : 전체 유닛과 아이템 등 게임 자체의 정보를 담고있는 데이터들입니다


    // 유저 데이터 관련
    [HideInInspector]
    public UserItemData userData_item;
    [HideInInspector]
    public UserSettingData userData_setting;
    
    private List<UserData> datas;

    // 게임 데이터 관련
    private UnitInfoManager gameData_Unit = UnitInfoManager.Instance;
    
    public static DataManager Instance;

    private void Awake()
    {
        Instance = this;

        userData_item = new UserItemData();
        userData_setting = new UserSettingData();

        datas = new List<UserData>();
        datas.Add(userData_item);
        datas.Add(userData_setting);

        DataTest(); // 임시 데이터 입력
    }

    public void DeleteEveryData()
    {
        for (int i = 0; i < datas.Count; ++i) datas[i].DeleteData();
    }

    // 해당 번호의 유닛을 만들 수 있는 최대 수량
    public int BuildableUnitCount(int unitIndex)
    {
        int count_money = userData_item.GetUserMoney() / gameData_Unit.unitList[unitIndex].buildMoney;           // 유저가 가진 돈/유닛 생성시 필요한 돈
        int count_material = userData_item.ItemCount(gameData_Unit.unitList[unitIndex].buildMaterial);   // 유저가 가진 해당 아이템의 수량

        if (count_money < 1 && count_material < 1) return -1;   // 돈과 아이템 둘 다 없는 경우
        if (count_money >= 1 && count_material < 1) return -2;  // 아이템이 없는 경우
        if (count_money < 1 && count_material >= 1) return -3;  // 돈이 없는 경우

        int count = count_money < count_material ? count_money : count_material;    // 둘 중 작은 숫자 반환
        return count;
    }

    public void DataTest()
    {
        DeleteEveryData();

        userData_item.EarnMoney(100);
        userData_item.EarnItem("test item", 10);
        userData_item.EarnItem("test item2", 5);

        userData_setting.SetSlot(0, 1);
        userData_setting.SetSlot(2, 1);
        userData_setting.SetSlot(3, 1);
        userData_setting.SetSlot(1, 2);

        userData_setting.SetSlot(2, 1, 2);
        userData_setting.SetActivatedSlotGroupIndex(2);
    }
}
