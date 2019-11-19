using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackTarget : MonoBehaviour
{
    public List<Status> enemyList;
    public GameObject target;
    public bool hasTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count > 0)
            EmptyClear();
        if (enemyList.Count > 0)
            SaerchTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemyList.Add(other.gameObject.GetComponent<Status>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
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
