using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{


    public Stack<ItemObject> slot;
    public Text cntItemText;        // 아이템에 개수 텍스트

    public Sprite DefaultImg;       // 기본 이미지(아무것도 없이 투명한 이미지)

    private Image ItemImg;
    private bool isSlotEmpty = true;     // 현재 슬롯이 비어있으면 true

    public ItemObject ItemReturn() { return slot.Peek(); }
    public bool IsSlotEmpty() { return isSlotEmpty; }
    public void SetSlots(bool isSlot) { this.isSlotEmpty = isSlot; }
    public bool ItemMax(ItemObject item) { return ItemReturn().MaxCount > slot.Count; } //아이템 최대치를 넘으면 false


    // Use this for initialization
    void Awake()
    {
        // 스택 메모리 할당.
        slot = new Stack<ItemObject>();

        //폰트 사이즈 설정
        float fSize = cntItemText.gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta.x;
        cntItemText.fontSize = (int)(fSize * 0.5f);

        ItemImg = transform.GetChild(0).GetComponent<Image>();
    }

    public void AddItem(ItemObject item)
    {
        slot.Push(item);
        isSlotEmpty = false;
        UpdateInfo(true, item.DefaultImg);
    }

    public void UseITem()
    {
        if (!isSlotEmpty) return;

        if(slot.Count == 1)
        {
            slot.Clear();
            UpdateInfo(false, DefaultImg);
            return;
        }

        slot.Pop();
        UpdateInfo(isSlotEmpty, ItemImg.sprite);
    }

    public void UpdateInfo(bool isSlot, Sprite sprite)
    {
        SetSlots(isSlot);
        ItemImg.sprite = sprite;
        cntItemText.text = slot.Count > 1 ? slot.Count.ToString() : "";
        //ItemIO.SaveData();
    }




}
