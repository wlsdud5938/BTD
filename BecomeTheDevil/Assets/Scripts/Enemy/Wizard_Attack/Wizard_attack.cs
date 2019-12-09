using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard_attack : MonoBehaviour
{
    FindAggroTarget aggroTarget;
    Attack attack;
    // Start is called before the first frame update
    void Start()
    {
        aggroTarget = transform.parent.GetComponent<FindAggroTarget>();
        attack = transform.parent.GetComponent<Attack>();

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Unit"))
        {
            if (aggroTarget.target == other.gameObject)
                attack.targetIn = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Unit"))
        {
            if (aggroTarget.target == other.gameObject)
                attack.targetIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Unit"))
        {
            if (aggroTarget.target == other.gameObject)
                attack.targetIn = false;
        }
    }


}