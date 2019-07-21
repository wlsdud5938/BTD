﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnitCreate : MonoBehaviour
{
    public GameObject target;
    public GameObject unit;
    Vector3 truePos;
    public float gridSize;
    public RaycastHit[] allHit;
    public int length;
    public int curUnitCount;
    public int curMaxUnit;
    public RoomInfo curRoom;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        allHit = Physics.RaycastAll(ray, 20.0f);
        //Here acceptableLayer is set to my Ground layer that's what I want to check against
        length = allHit.Length;
        string tag = allHit[0].collider.tag;
        if ((allHit.Length == 2 && allHit[0].collider.CompareTag("Map") && allHit[1].collider.CompareTag("Map")))
        {
            int i = 0;

            if (allHit[0].collider.name == "Plane")
                i = 1;
            else i = 0;

            curRoom = allHit[i].collider.gameObject.GetComponent<RoomInfo>();
            curUnitCount = curRoom.unitList.Count;
            curMaxUnit = curRoom.maxUnit;
            int x = Mathf.FloorToInt(allHit[i].point.x / gridSize);
            int z = Mathf.FloorToInt(allHit[i].point.z / gridSize);

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
