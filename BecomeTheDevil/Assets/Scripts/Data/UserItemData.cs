using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserItemData : UserData
{
    // 유저의 아이템 데이터 클래스

    private Dictionary<string, int> userItemList;    // 유저가 가지고 있는 아이템 리스트
    private int userMoney;                           // 유저가 가지고 있는 돈(기본 정수)

    private const string identifier_userItem = "identifier_userItem";
    private const string identifier_userMoney = "identifier_userMoney";

    public UserItemData()
    {
        userItemList = new Dictionary<string, int>();
        userMoney = 0;
        LoadData();
    }

    public int GetUserMoney() { return userMoney; }

    public void EarnMoney(int amount)
    {
        userMoney += amount;
        Save<int>(identifier_userMoney, userMoney);
    }

    public void UseMoney(int amount)
    {
        if (userMoney < amount) return;
        userMoney -= amount;
        Save<int>(identifier_userMoney, userMoney);
    }

    public void EarnItem(string itemName, int amount = 1)    // 해당 아이템과 수량 추가 및 저장
    {
        if (userItemList.ContainsKey(itemName)) userItemList[itemName] += amount;  
        else userItemList.Add(itemName, amount);

        Save<Dictionary<string, int>>(identifier_userItem, userItemList);
    }

    public bool UseItem(string itemName, int amount = 1)    // 해당 아이템을 수량만큼 사용 및 저장
    {
        if (ItemCount(itemName) == 0) return false;   // 아이템 없으면 종료

        userItemList[itemName] -= amount;
        Save<Dictionary<string, int>>(identifier_userItem, userItemList);
        return true;
    }

    public int ItemCount(string itemName)    // 해당 아이템의 갯수 반환
    {
        if (!userItemList.ContainsKey(itemName)) return 0;   
        return userItemList[itemName];
    }

    public override void SaveData()
    {
        Save<Dictionary<string, int>>(identifier_userItem, userItemList);
        Save<int>(identifier_userMoney, userMoney);
    }

    public override void LoadData()
    {
        userItemList = Load<Dictionary<string, int>>(identifier_userItem);
        userMoney = Load<int>(identifier_userMoney);
    }

    public override void DeleteData()
    {
        Delete(identifier_userItem);
        Delete(identifier_userMoney);
        userItemList = new Dictionary<string, int>();
        userMoney = 0;
    }
}
