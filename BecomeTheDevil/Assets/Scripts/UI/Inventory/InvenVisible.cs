﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InvenVisible : MonoBehaviour {

    public static bool isInvenOpen = false;   //아이템창이 열려 있으면 true, 아니면 false
    public GameObject iv;    //아이템창

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInvenOpen)
            {
                OpenInven();
            }
            else
            {
                CloseInven();
            }
        }

        if (!iv.activeSelf)
        {
            isInvenOpen = false;
        }
    }

    void OpenInven()
    {
        iv.SetActive(true);  //인벤토리가 보이게

        isInvenOpen = true;
    }

    void CloseInven()
    {
        iv.SetActive(false); //인벤토리 감추기

        isInvenOpen = false;
    }
}
