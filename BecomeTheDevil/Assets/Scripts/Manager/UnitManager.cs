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
        if(isBuild)
        {
            if (Input.GetKeyDown(KeyCode.B))
                isBuild = false;
            if(Input.GetMouseButtonDown(0)&&grid.length == 2)
                MouseDown();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.B))
                isBuild = true;
        }
    }

    private void MouseDown()
    {
        pos = new Vector3(unitPos.position.x, 0.0f, unitPos.position.z);
        Instantiate(unit, pos, Quaternion.Euler(90.0f, 0.0f, 0.0f));
    }
}
