using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSlot : MonoBehaviour
{
    public Image contentImage;  // 슬롯 내용 이미지
    public Text amountText;     // 수량 표시 텍스트
    public Image[] colorChangeImage;    // 선택되었을 때 녹색으로 변하는 이미지들

    [HideInInspector]
    public bool isBuildable;    // 해당 슬롯 유닛의 건설가능 여부
    [HideInInspector]
    public int index, amount;

    private bool selected = false;
    private static Color32 activeColor = new Color(0.75f, 0.75f, 0.75f);
    private static Color32 deactiveColor = new Color(0.3f, 0.3f, 0.3f);
    private static Color32 selectedColor = new Color(0.16f, 1.0f, 0.55f);

    public void ShowSlotContent(bool isBuildMode, int unitIndex)
    {
        index = unitIndex;

        if (isBuildMode)
        {
            // 건설 모드의 경우 유닛이 표시됨
            // 해당 유닛의 이미지 불러와 입히기
            contentImage.gameObject.SetActive(true);
            contentImage.sprite = UnitInfoManager.Instance.unitList[unitIndex].unitSprite;
            contentImage.SetNativeSize();

            amount = DataManager.Instance.BuildableUnitCount(unitIndex);    // 유닛 건설가능 수량 계산
            
            if(amount <= 0)
            {
                SlotDeactivate();
                amountText.text = "0";
            }
            else
            {
                SlotActivate();
                amountText.text = amount.ToString();
            }
        }
        else if (!isBuildMode)
        {
            // 전투 모드의 경우 아이템이 표시됨
            contentImage.gameObject.SetActive(true);
            contentImage.sprite = ItemInfoManager.Instance.itemList[unitIndex].itemSprite;
            contentImage.SetNativeSize();

            int itemCount = DataManager.Instance.userData_item.ItemCount(ItemInfoManager.Instance.itemList[unitIndex].itemName);
            amountText.text = itemCount.ToString();

            if (itemCount == 0) SlotDeactivate();
            else SlotActivate();
        }
    }

    public void SlotOnOff()
    {
        selected = !selected;
        if (selected)    for (int i = 0; i < colorChangeImage.Length; ++i) colorChangeImage[i].color = selectedColor;
        else if (!selected) for (int i = 0; i < colorChangeImage.Length; ++i) colorChangeImage[i].color = Color.white;
    }

    public void SlotActivate()
    {
        contentImage.color = activeColor;
        isBuildable = true;
    }

    public void SlotDeactivate()
    {
        contentImage.color = deactiveColor;
        isBuildable = false;
    }

    public void SlotInit()
    {
        contentImage.gameObject.SetActive(false);
        amountText.text = "";
    }
}
