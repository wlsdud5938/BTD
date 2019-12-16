using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackTarget : MonoBehaviour
{
    public FindAggroTarget aggroTarget;
    Attack attack;
    // Start is called before the first frame update
    void Start()
    {
        aggroTarget = transform.parent.GetComponent<FindAggroTarget>();
        attack = transform.parent.GetComponent<Attack>();
    }
    private void Update()
    {
        if (aggroTarget.enemyList.Count > 0)
            attack.targetIn = true;
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("Enemy") ||other.CompareTag("Field")) && gameObject.transform.parent.CompareTag("Unit"))
        {
            aggroTarget.AddList(other.gameObject);
        }
        else if (gameObject.transform.parent.CompareTag("Field"))
        {
            if(other.CompareTag("Enemy") || other.CompareTag("Unit") || other.CompareTag("Player"))
                aggroTarget.AddList(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("Field")) && gameObject.transform.parent.CompareTag("Unit"))
        {
            aggroTarget.RemoveList(other.gameObject);

        }
        else if (gameObject.transform.parent.CompareTag("Field"))
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Unit") || other.CompareTag("Player"))
                aggroTarget.RemoveList(other.gameObject);
        }
    }
    
}
