using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public bool isBuild = false;
    public GameObject[] units;
    public GameObject unitPosition;
    public GameObject Grid;
    public Transform unitPos;
    public GameObject[] unitList;
    public GameObject[] unitShow;
    public int currentUnit = 1;
    public int num = 0;
    Vector3 pos;
    public GridUnitCreate grid;
    int lastUnit = 0;
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
        if (Input.GetMouseButtonDown(0))
        {
            if(isBuild && grid.length == 2 && (grid.curMaxUnit>grid.curRoom.unitList.Count))
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
        pos = new Vector3(unitPos.position.x, 0.0f, unitPos.position.z);
        int i = 0;
        for(i=0;i<grid.curMaxUnit;i++)
        {
            if (grid.curRoom.unitList.Count < grid.curMaxUnit)
                break;
            if (grid.curRoom.unitList[i] == null)
                break;
        }
        GameObject newUnit = Instantiate(units[currentUnit-1], pos, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        newUnit.GetComponent<Unit>().unitNum = i;
        newUnit.GetComponent<Status>().curRoom = grid.curRoom.gameObject;
        
        grid.curRoom.unitList.Add(newUnit);
        
    }
}
