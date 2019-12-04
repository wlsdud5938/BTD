using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnitCreate : MonoBehaviour
{
    public GameObject target;       // 보여지는 유닛의 위치를 지정해줍니다.(현재 조정이 살짝 필요합니다.)
    public GameObject unit;         // 보여질 유닛들의 부모오브젝트
    public float gridSize;          // 칸의 크기를 지정합니다(기본으로 1로 지정합니다)
    public RaycastHit[] allHit;     // 마우스가 클릭한 모든 것을 배열에 저장합니다.
    public int length;              // allHit의 길이를 알아보기위해 public으로 뺐습니다.
    public int curUnitCount;        // 현재 방의 유닛수를 지정해줍니다.
    public int curMaxUnit;          // 현재 방의 최대 유닛수를 알려줍니다.
    public RoomInfo curRoom;        // 현재방의 roominfo를 받아옵니다.
    bool otherColl = true;          // 마우스가 클릭했을때 그 위치에 다른 객체가 존재하는지 판단하는 bool변수입니다.
    int mapNum = -1;                // map이 몇번째 allHit에 존재하는지 알려주는 변수입니다. -1일때 map이 잡히지 않은것 입니다.
    
    void Update()
    {
        //Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    //스크린에 클릭한 위치를 잡아줍니다.
        allHit = Physics.RaycastAll(ray, 20.0f);    //allHit에 물리적으로 잡힌 모든 오브젝트를 allHit에 저장합니다.
        //Here acceptableLayer is set to my Ground layer that's what I want to check against
        length = allHit.Length;
        mapNum = -1;
        otherColl = true;
        //allHit에 적이나 아군 플레이어 벽이 잡힌다면 유닛이미지는 따라오지 않습니다.
        for(int i=0;i<length;i++)
        {
            string tag = allHit[i].collider.tag;
            if (tag == "Enemy" || tag == "Player" || tag == "Wall" || tag == "Unit")
            {
                otherColl = false;
                break;
            }
            if (tag == "Map")
                mapNum = i;
        }
        //현재 위치에 맞춰 유닛 이미지를 이동시킵니다.
        if (otherColl && mapNum != -1)
        {
            
            curRoom = allHit[mapNum].collider.gameObject.GetComponent<RoomInfo>();
            curUnitCount = curRoom.unitList.Count;
            curMaxUnit = curRoom.maxUnit;
            int x = Mathf.FloorToInt(allHit[mapNum].point.x / gridSize);
            int z = Mathf.FloorToInt(allHit[mapNum].point.z / gridSize);

            unit.transform.position = new Vector3(x * gridSize, 0, z * gridSize);
            
        }



        //    // Update is called once per frame
        //    void LateUpdate()
        //{
        //    truePos.x = Mathf.Floor(Input.mousePosition.x / gridSize) * gridSize;
        //    truePos.y = 0;
        //    truePos.z = Mathf.Floor(Input.mousePosition.y / gridSize) * gridSize;

        //    unit.transform.position = truePos;
        //}

    }
}
