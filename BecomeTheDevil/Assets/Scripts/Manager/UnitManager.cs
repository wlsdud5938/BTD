using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public bool isBuild = false;    //유닛 생성 가능 유무 확인
    public GameObject[] units;  //생성할 유닛의 종류 넣어둔 리스트
    public GameObject unitPosition; //유니티 내의 Grid 게임 오브젝트 하위의 마우스따라다니는 유닛 위치(위치 조정이 조금 필요함)
    public GameObject Grid; //유니티 내의 Grid 게임 오브젝트 - 태그로 받아오면 이거때매 또 태그만들어야하고 매 씬마다 오버헤드가생겨 귀찮아서 그냥 유니티내에서 넣어줌
    public Transform unitPos;   //유닛이 생성될 위치
    public GameObject[] unitShow;   //생성할 유닛의 스프라이트만 표시
    public int currentUnit = 1; //현재 생성될(표시된) 유닛의 번호(사람들은 1부터 누르기때문에 1로 초기화해서 사용)
    public int num = 0; //현재 누른 번호
    Vector3 pos;    //유닛 포지션에서 y를 0으로 초기화한 위치
    public GridUnitCreate grid; //유닛 생성에 필요한 위치를 잡아주는 오브젝트
    int lastUnit = 0;   //이전에 선택되어있던 유닛을 가지고있는 변수
    // Start is called before the first frame update
    void Start()
    {
        currentUnit = 1;
        unitPos = unitPosition.transform;
        grid = Grid.GetComponent<GridUnitCreate>();
    }

    // Update is called once per frame
    void Update()
    {
        //키 입력을 받으면 num에 저장
        if (Input.GetKeyDown(KeyCode.Alpha1))
            num = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            num = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            num = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4))
            num = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5))
            num = 5;
        if (Input.GetKeyDown(KeyCode.Alpha6))
            num = 6;
        //마우스 입력을 받았을때 유닛 생성 모드이고 현재 맵의 유닛이 현재 맵의 최대 유닛수 보다 작다면 함수 호출
        if (Input.GetMouseButtonDown(0))
        {
            if(isBuild &&  (grid.curMaxUnit>grid.curRoom.unitList.Count))
                MouseDown();
        }
        if (Input.GetMouseButtonDown(1) && !isBuild)
        {
            isBuild = true;
            unitShow[currentUnit - 1].gameObject.SetActive(true);
        }
        else if (isBuild && Input.GetMouseButtonDown(1))
        {
            isBuild = false;
            unitShow[currentUnit - 1].gameObject.SetActive(false);
        }

        if(isBuild && num != 0)
        {
            unitShow[currentUnit - 1].gameObject.SetActive(false);
            currentUnit = num;
            unitShow[currentUnit - 1].gameObject.SetActive(true);
        }

        num = 0;
    }

    private void MouseDown()
    {
        //마우스 포인터 위치의 그리드에 y축이 0인 vector3값
        pos = new Vector3(unitPos.position.x, 0.0f, unitPos.position.z);
        int i = 0;  //for 밖에서도 사용되어야하기때문에 for위에 선언

        //현재 맵에 유닛 리스트에 새로운 유닛 추가(null체크의 이유는 비정상적인 방법으로 유닛이 지워졌을시를 대비)
        for(i=0;i<grid.curMaxUnit;i++)
        {
            if (grid.curRoom.unitList.Count < grid.curMaxUnit)
                break;
            if (grid.curRoom.unitList[i] == null)
                break;
        }
        GameObject newUnit = Instantiate(units[currentUnit-1], pos, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        newUnit.GetComponent<AggroType>().aggroType = i;    //해당 유닛의 어그로 타입을 지정(유닛 테이블에 유닛 코드넘버링 추가 필요)
        newUnit.GetComponent<FindAggroTarget>().currentRoom = grid.curRoom.gameObject.GetComponent<RoomInfo>(); //해당 유닛의 생성된 방의 roominfo 지정(ai등 다양한 곳에 사용됨)
        
        grid.curRoom.unitList.Add(newUnit);
        
    }
}
