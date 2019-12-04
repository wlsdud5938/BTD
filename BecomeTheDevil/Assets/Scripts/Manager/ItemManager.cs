using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private ItemInfo[][] itemPool;    // 아이템 오브젝트 풀
    private int itemKind;               // 총 아이템 종류
    private int[] itemPointer;          // 아이템 풀 포인터

    public static ItemManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MakePool();
    }

    // 확률로 아이템 드롭
    public void ItemDrop(Vector3 position, int itemIndex, int chance)
    {
        //없는 아이템을 호출 시 리턴
        if (itemIndex >= itemPool.Length || itemIndex < 0) return;

        // 확률 실패시 리턴
        if (Random.Range(0, 100) > chance) return;

        // 풀에서 아이템 지정 및 해당 아이템 드랍
        ItemInfo item = itemPool[itemIndex][itemPointer[itemIndex]];
        item.Drop(position);

        // 포인터 이동
        itemPointer[itemIndex] = itemPointer[itemIndex] == itemPool[itemIndex].Length - 1 ? 0 : itemPointer[itemIndex] + 1;
    }

    public void TestDrop()
    {
        ItemDrop(transform.position, 0, 90);
        ItemDrop(transform.position, 0, 90);
        ItemDrop(transform.position, 0, 90);
        ItemDrop(transform.position, 1, 90);
        ItemDrop(transform.position, 2, 90);
        ItemDrop(transform.position, 3, 90);
        ItemDrop(transform.position, 4, 90);
    }

    // 아이템 오브젝트 풀 생성
    private void MakePool()     
    {
        // ItemInfoManager에 있는 아이템 종류만큼 풀 생성
        itemKind = ItemInfoManager.Instance.itemList.Length;
        itemPool = new ItemInfo[itemKind][];
        itemPointer = new int[itemKind];

        // 각 아이템별로 풀 생성, 0번인 money의 풀은 좀 더 크게 생성(많이 등장하므로)
        itemPool[0] = new ItemInfo[30];   
        for (int i = 1; i < itemKind; ++i) itemPool[i] = new ItemInfo[10];    

        // 풀 채워넣기
        for (int i = 0; i < itemKind; ++i)
        {
            itemPointer[i] = 0;
            for (int j = 0; j < itemPool[i].Length; ++j)
            {
                itemPool[i][j] = Instantiate(ItemInfoManager.Instance.itemList[i].GetComponent<ItemInfo>(), transform);
                itemPool[i][j].gameObject.SetActive(false);
            }
        }
    }
}
