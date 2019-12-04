using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfoManager : MonoBehaviour
{
    public ItemInfo[] itemList;     // 아이템 정보를 담은 프리팹의 리스트, 아이템 고유넘버 순서

    public static ItemInfoManager Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int GetItemIndexByName(string name)
    {
        for (int i = 0; i < itemList.Length; ++i) if (name.CompareTo(itemList[i].itemName) == 0) return itemList[i].itemIndex;
        return -1;
    }
}
