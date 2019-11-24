using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Slots : MonoBehaviour
{

    public List<GameObject> All_Unit_Slot;    // 모든 슬롯을 관리해줄 리스트.
    public RectTransform InvenRect;     // 인벤토리의 Rect
    public GameObject Origin_Unit_Slot;       // 오리지널 슬롯.

    public float slotSize;              // 슬롯의 사이즈.
    //public float slotGap;               // 슬롯간 간격.
    public float slotCountX;            // 슬롯의 가로 개수.
    public float slotCountY;            // 슬롯의 세로 개수.

    // Start is called before the first frame update
    void Awake()
    {
        GameObject slot = Instantiate(Origin_Unit_Slot) as GameObject;
        slotSize = 82f;
        slotCountX = 6f;
    }

    // Update is called once per frame
    void Update()
    {

    }


    // 거리가 가까운 슬롯의 반환.
    public Unit_Slot NearDisSlot(Vector3 Pos)
    {
        float Min = 10000f;
        int Index = -1;

        int Count = All_Unit_Slot.Count;
        for (int i = 0; i < Count; i++)
        {
            Vector2 sPos = All_Unit_Slot[i].transform.GetChild(2).position; // Contents_image 의 포지션
            float Dis = Vector2.Distance(sPos, Pos);

            if (Dis < Min)
            {
                Min = Dis;
                Index = i;
            }
        }

        if (Min > slotSize)
            return null;

        return All_Unit_Slot[Index].GetComponent<Unit_Slot>();
    }

    // 아이템 옮기기 및 교환.
    public void Swap(Unit_Slot slot, Vector3 Pos)
    {
        Unit_Slot FirstSlot = NearDisSlot(Pos);

        // 현재 슬롯과 옮기려는 슬롯이 같으면 함수 종료.
        if (slot == FirstSlot || FirstSlot == null)
        {
            slot.UpdateInfo(true, slot.slot.Peek().DefaultImg);
            return;
        }

        // 가까운 슬롯이 비어있으면 옮기기.
        if (!FirstSlot.isSlots())
        {
            //Swap(FirstSlot, slot);
        }
        // 교환.
        else
        {
            int Count = slot.slot.Count;
            ItemObject item = slot.slot.Peek();
            Stack<ItemObject> temp = new Stack<ItemObject>();

            {
                for (int i = 0; i < Count; i++)
                    temp.Push(item);

                slot.slot.Clear();
            }

            Swap(slot, FirstSlot);

            {
                Count = temp.Count;
                item = temp.Peek();

                for (int i = 0; i < Count; i++)
                    FirstSlot.slot.Push(item);

                FirstSlot.UpdateInfo(true, temp.Peek().DefaultImg);
            }
        }
    }

    // 1: 비어있는 슬롯, 2: 안 비어있는 슬롯.
    void Swap(Unit_Slot xFirst, Unit_Slot oSecond)
    {
        int Count = oSecond.slot.Count;
        ItemObject item = oSecond.slot.Peek();

        for (int i = 0; i < Count; i++)
        {
            if (xFirst != null)
                xFirst.slot.Push(item);
        }

        if (xFirst != null)
            xFirst.UpdateInfo(true, oSecond.ItemReturn().DefaultImg);

        oSecond.slot.Clear();
        oSecond.UpdateInfo(false, oSecond.DefaultImg);
    }
}
