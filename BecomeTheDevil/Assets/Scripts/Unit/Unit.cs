﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int unitNum;
    float time =5.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
            time -= 1*Time.deltaTime;
        else
        {
            GameObject.FindGameObjectWithTag("Grid").GetComponent<GridUnitCreate>().curRoom.unitList.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}