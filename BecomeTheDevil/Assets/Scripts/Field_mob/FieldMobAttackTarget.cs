using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMobAttackTarget : MonoBehaviour
{
    public List<Status> enemyList;
    public GameObject target;
    public bool hasTarget = false;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count > 0)
            EmptyClear();
        if (enemyList.Count > 0)
            SaerchTarget();
        if (enemyList.Count == 0)
            ani.SetBool("Idle", true);
        else
            ani.SetBool("Idle", false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Player")|| other.CompareTag("Unit"))
        {
            enemyList.Add(other.gameObject.GetComponent<Status>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player") || other.CompareTag("Unit"))
        {
            enemyList.Remove(other.gameObject.GetComponent<Status>());
        }
    }

    void SaerchTarget()
    {
        GameObject tempTarget = null;
        float tempAggroStack = 0;
        for(int i = 0; i <enemyList.Count;i++)
        {
            if(enemyList[i].objAggroStack>tempAggroStack)
            {
                tempAggroStack = enemyList[i].objAggroStack;
                tempTarget = enemyList[i].gameObject;
            }
        }
        if (tempTarget == null)
            hasTarget = false;
        else
        {
            target = tempTarget;
            hasTarget = true;
        }
    }

    void EmptyClear()
    {
        
        for(int i=0;i< enemyList.Count;i++)
        {
            if(enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
                i--;

            }
        }
    }

}
