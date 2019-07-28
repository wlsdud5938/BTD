﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 curLocation;
    private Vector2 preLocation;

    private Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        preLocation = transform.position;
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        curLocation = transform.position;
        if(preLocation.x <= curLocation.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        preLocation = transform.position;
    }
}
