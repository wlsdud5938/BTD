using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public bool isBuild = false;
    public GameObject unit;
    public GameObject unitPosition;
    public GameObject Grid;
    public Transform unitPos;
    Vector3 pos;
    public GridUnitCreate grid;
    // Start is called before the first frame update
    void Start()
    {
        unitPos = unitPosition.transform;
        grid = Grid.GetComponent<GridUnitCreate>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isBuild && grid.length == 2 && (grid.curMaxUnit>grid.curRoom.unitList.Count))
                MouseDown();
        }
        if (isBuild && Input.GetKeyDown(KeyCode.B))
            isBuild = false;
        else if(!isBuild && Input.GetKeyDown(KeyCode.B))
            isBuild = true;
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
        GameObject newUnit = Instantiate(unit, pos, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        newUnit.GetComponent<Unit>().unitNum = i;
        newUnit.GetComponent<Status>().curRoom = grid.curRoom.gameObject;
        
        grid.curRoom.unitList.Add(newUnit);
        
    }
}
