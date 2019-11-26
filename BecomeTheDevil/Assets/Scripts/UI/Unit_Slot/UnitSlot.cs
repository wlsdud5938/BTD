using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSlot : MonoBehaviour
{
    public Image contentImage;  // 슬롯 내용 이미지
    public Text amountText;     // 수량 표시 텍스트

    [HideInInspector]
    public bool isBuildable;    // 해당 슬롯 유닛의 건설가능 여부
    [HideInInspector]
    public int index, amount;

    private static Color32 activeColor = new Color(0.75f, 0.75f, 0.75f);
    private static Color32 deactiveColor = new Color(0.3f, 0.3f, 0.3f);

    public void ShowSlotContent(bool isBuildMode, int unitIndex)
    {
        index = unitIndex;

        if (isBuildMode)
        {
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
