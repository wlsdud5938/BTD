﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAttack : MonoBehaviour
{
    private Animator myAnimator;
    private GameObject root;
    Attack attack;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.parent.transform.parent.GetComponent<Animator>();
        root = transform.root.gameObject;

        attack = transform.root.GetComponent<Attack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == attack.target)
        {
            myAnimator.SetBool("Attack", true);
            myAnimator.SetFloat("AttackX", -1.0f);
            myAnimator.SetFloat("AttackY", -1.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == attack.target)
        {
            myAnimator.SetBool("Attack", false);
        }
    }
}
