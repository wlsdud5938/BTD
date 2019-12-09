using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public bool isBuild = false;            // 빌드모드 여부 확인
    public GameObject unitPosition;         // 유니티 내의 Grid 게임 오브젝트 하위의 마우스따라다니는 유닛 위치(위치 조정이 조금 필요함)
    public GameObject Grid;                 // 유니티 내의 Grid 게임 오브젝트 - 태그로 받아오면 이거때매 또 태그만들어야하고 매 씬마다 오버헤드가생겨 귀찮아서 그냥 유니티내에서 넣어줌
    public GameObject unitIcon;             // 마우스 따라다니는 유닛 아이콘
    public GameObject[] unitShow;           // 생성할 유닛의 스프라이트만 표시
    public int currentUnit = 1;             // 현재 생성될(표시된) 유닛의 번호(사람들은 1부터 누르기때문에 1로 초기화해서 사용)
    Vector3 pos;                            // 유닛 포지션에서 y를 0으로 초기화한 위치

    [HideInInspector]
    public GridUnitCreate grid;             // 유닛 생성에 필요한 위치를 잡아주는 오브젝트
    
    private UnitInfo[][] unitPool;          // 유닛 오브젝트 풀
    private int[] unitPointer;              // 유닛 풀 포인터

    void Start()
    {
        currentUnit = 1;
        grid = Grid.GetComponent<GridUnitCreate>();
        
        MakePool(10);
    }
    
    public void BuildOnOff()
    {
        // 마우스 입력을 받았을때 유닛 생성 모드이고 현재 맵의 유닛이 현재 맵의 최대 유닛수 보다 작다면 함수 호출
        if (Input.GetMouseButtonDown(0))
        {
            if(isBuild /*&& (grid.curMaxUnit > grid.curRoom.unitList.Count)*/)
            {
                MouseDown();
            }
        }

        // 빌드모드 활성화
        if (Input.GetMouseButtonDown(1) && !isBuild)
        {
            isBuild = true;
            unitIcon.SetActive(true);
            UnitSlotManager.Instance.SetSlots(isBuild);
        }
        // 빌드모드 끄기
        else if (isBuild && Input.GetMouseButtonDown(1))
        {
            isBuild = false;
            unitIcon.SetActive(false);
            UnitSlotManager.Instance.SetSlots(isBuild);
        }
    }

    private void MouseDown()
    {
        // 충분한 재료가 없으면 리턴
        if (DataManager.Instance.BuildableUnitCount(DataManager.Instance.userData_setting.GetSelectedIndex()) <= 0) return;

        int i = 0;  //for 밖에서도 사용되어야하기때문에 for위에 선언

        //현재 맵에 유닛 리스트에 새로운 유닛 추가(null체크의 이유는 비정상적인 방법으로 유닛이 지워졌을시를 대비)
        for(i=0;i<grid.curMaxUnit;i++)
        {
            if (grid.curRoom.unitList.Count < grid.curMaxUnit)
                break;
            if (grid.curRoom.unitList[i] == null)
                break;
        }

        GameObject newUnit = PullUnit();
        if (newUnit == null) return;
        newUnit.SetActive(true);
        newUnit.transform.position = unitPosition.transform.position;

        // 필요한 돈과 아이템 소모
        DataManager.Instance.UseUnitBuildMaterial(DataManager.Instance.userData_setting.GetSelectedIndex());
        // 슬롯 표시 업데이트
        UnitSlotManager.Instance.SetSlots();

        newUnit.GetComponent<AggroType>().aggroType = i;    //해당 유닛의 어그로 타입을 지정(유닛 테이블에 유닛 코드넘버링 추가 필요)
        newUnit.GetComponent<FindAggroTarget>().currentRoom = grid.curRoom.gameObject.GetComponent<RoomInfo>(); //해당 유닛의 생성된 방의 roominfo 지정(ai등 다양한 곳에 사용됨)
        
        grid.curRoom.unitList.Add(newUnit);        
    }

    private void MakePool(int poolSize)
    {
        // 전체 유닛 종류만큼 풀 생성 및 채우기
        int unitCount = UnitInfoManager.Instance.unitList.Length;
        unitPool = new UnitInfo[unitCount][];
        unitPointer = new int[unitCount];

        for (int i = 0; i < unitCount; ++i)
        {
            unitPool[i] = new UnitInfo[poolSize];
            unitPointer[i] = 0;

            for (int j = 0; j < poolSize; ++j)
            {
                unitPool[i][j] = Instantiate(UnitInfoManager.Instance.unitList[i], transform);
                unitPool[i][j].gameObject.SetActive(false);
            }
        }
    }

    private GameObject PullUnit()
    {
        // 데이터매니져에서 현재 선택된 유닛 인덱스 가져와 풀에서 선택
        int unitIndex = DataManager.Instance.userData_setting.GetSelectedIndex();
        int currentPointer = unitPointer[unitIndex];
        for (int i = currentPointer; i < currentPointer + unitPool[unitIndex].Length; ++i)
        {
            // 포인터 이동
            unitPointer[unitIndex] = unitPointer[unitIndex] == unitPool[unitIndex].Length - 1 ? 0 : unitPointer[unitIndex] + 1;
            // 활성화되지 않은(배치되지 않은) 오브젝트면 반환
            if (!unitPool[unitIndex][unitPointer[unitIndex]].gameObject.activeInHierarchy)
                return unitPool[unitIndex][unitPointer[unitIndex]].gameObject;
        }

        Debug.Log("풀을 더 키우세요");
        return null;
    }    
}
