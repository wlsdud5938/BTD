using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inven : MonoBehaviour
{

    public List<GameObject> AllSlot; //슬롯을 관리해줄 리스트
    public RectTransform InvenRect;
    public GameObject OriginSlot;

    public GameObject SlotParent;

    public float slotSize = 130f;              // 슬롯의 사이즈.
    public float slotGapX = 24f;               // 슬롯간 가로 간격.
    public float slotGapY = 42f;              //슬롯간 세로 간격.
    public float slotCountX = 6;            // 슬롯의 가로 개수.
    public float slotCountY = 3;            // 슬롯의 세로 개수.

    private float InvenWidth;           // 인벤토리 가로길이.
    private float InvenHeight;          // 인밴토리 세로길이.

    private float EmptySlot;            // 빈 슬롯의 개수.

    public List<GameObject> GetAllSlot() { return AllSlot; }


    // Use this for initialization
    // 시작하자마자 인벤토리 생성
    void Awake()
    {

        // 인벤토리 이미지의 가로, 세로 사이즈 셋팅.
        InvenWidth = (slotCountX * slotSize) + (slotCountX -1)* slotGapX + 32 + 32;
        InvenHeight = (slotCountY * slotSize) + (slotCountY - 1) * slotGapY + 92 + 68;

        // 셋팅된 사이즈로 크기를 설정.
        InvenRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, InvenWidth); // 가로.
        InvenRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, InvenHeight);  // 세로.

        // 슬롯 생성하기.
        for (int y = 0; y < slotCountY; y++)
        {
            for (int x = 0; x < slotCountX; x++)
            {
                // 슬롯을 복사한다.
                GameObject slot = Instantiate(OriginSlot) as GameObject;
                // 슬롯의 RectTransform을 가져온다.
                RectTransform slotRect = slot.GetComponent<RectTransform>();
                // 슬롯의 자식인 투명이미지의 RectTransform을 가져온다.
                RectTransform item = slot.transform.GetChild(0).GetComponent<RectTransform>();

                slot.name = "slot_" + y + "_" + x; // 슬롯 이름 설정.
                slot.transform.parent = SlotParent.transform; // 슬롯의 부모를 설정

                // 슬롯이 생성될 위치 설정하기.
                slotRect.localPosition = new Vector3((slotSize * x) + (slotGapX * x) + 32,
                                                   -((slotSize * y) + (slotGapY * y) + 92),
                                                      0);

                // 슬롯의 자식인 투명이미지의 사이즈 설정하기.
                slotRect.localScale = Vector3.one;
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize); // 가로
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);   // 세로.

                // 슬롯의 사이즈 설정하기.
                item.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize - slotSize * 0.3f); // 가로.
                item.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize - slotSize * 0.3f);   // 세로.

                // 리스트에 슬롯을 추가.
                AllSlot.Add(slot);
            }
        }

        // 빈 슬롯 = 슬롯의 숫자.
        EmptySlot = AllSlot.Count;

        Invoke("Init", 0.1f);

    }

    public bool AddItemInSlot(ItemObject item)
    {

        int slotCount = AllSlot.Count;

        for (int i = 0; i < slotCount; i++)
        {

            InvenSlot slot = AllSlot[i].GetComponent<InvenSlot>();
            
            if (slot.IsSlotEmpty()) continue;

            if (slot.ItemReturn().type == item.type && slot.ItemMax(item))
            {
                slot.AddItem(item);
                Debug.Log("Add item success");
                return true;
            }
        }
        
        for(int i = 0; i<slotCount; i++)
        {
            InvenSlot slot = AllSlot[i].GetComponent<InvenSlot>();

            if (!slot.IsSlotEmpty()) continue;

            slot.AddItem(item);
            Debug.Log("Add item success");
            return true;
        }

        Debug.Log("Add item fails");
        return false;
    }
    

}
