﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAttack : MonoBehaviour
{

    private Animator myAnimator;
    private GameObject root;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("????");
        myAnimator = transform.parent.transform.parent.GetComponent<Animator>();
        root = transform.parent.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.tag == "Player" || other.transform.root.tag == "Unit")
        {
            Debug.Log(root);
            root.transform.localScale = new Vector3(1, 1, 1);
            myAnimator.SetTrigger("Attack");
            myAnimator.SetFloat("DirX", 1.0f);
            //myAnimator.SetFloat("AttackY", 0.0f);
        }
    }
}