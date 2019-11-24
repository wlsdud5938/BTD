using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Slot_Change : MonoBehaviour
{
    //public Unit_Slots us;
    public Transform Img;   // 빈 이미지 객체.

    private Image EmptyImg; // 빈 이미지.
    private Unit_Slot slot;      // 현재 슬롯에 스크립트

    private bool checkClick = false;    // 이미 한 개의 슬롯을 선택했는지 안했는지
    

    // Start is called before the first frame update
    void Start()
    {
        // 현재 슬롯의 스크립트를 가져온다.
        slot = GetComponent<Unit_Slot>();
        // 빈 이미지 객체를 태그를 이용하여 가져온다.
        Img = GameObject.FindGameObjectWithTag("DragImg").transform;
        // 빈 이미지 객체가 가진 Image컴포넌트를 가져온다.
        EmptyImg = Img.GetComponent<Image>();
    }

    public void Down()
    {
        Debug.Log("Down 누름");
        // 슬롯에 아이템이 없으면 함수종료.
        if (!slot.isSlots())
        {
            Debug.Log("슬롯에 아이템이 없으면 함수종료");
            return;
        }
            

        // 처음 슬롯을 선택하는 경우.
        if (!checkClick)
        {
            checkClick = true;
            // 빈 이미지의 스프라이트를 슬롯의 스프라이트로 변경한다.
            //EmptyImg.sprite = slot.ItemReturn().DefaultImg;
            // 빈 이미지의 위치를 마우스위로 가져온다.
            //Img.transform.position = Input.mousePosition;
        }

        // 슬롯을 두 번째 선택하는 경우 Swap
        else if (checkClick)
        {
            // isSlots플래그가 false이면 슬롯에 아이템이 존재하지 않는 것이므로 함수 종료.
            if (!slot.isSlots())
                return;

            Debug.Log("스왑함수 호출");
            Img.transform.position = Input.mousePosition;
            // 싱글톤을 이용해서 인벤토리의 스왑함수를 호출(현재 슬롯, 빈 이미지의 현재 위치.)
            ObjectManager.Call().unitSlots.Swap(slot, Img.transform.position);
            checkClick = false;
            //slot = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
