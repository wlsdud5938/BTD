using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int unitNum;
    Status status;
    float currentHP;
    public GameObject attackTarget;
    UnitAttackTarget getTarget;
    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<Status>();
        currentHP = status.curHP;
        getTarget = transform.GetChild(0).GetComponent<UnitAttackTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        if (getTarget.hasTarget)
            attackTarget = getTarget.target;
        currentHP = status.curHP;
        if (currentHP <= 0)
            die();
    }


    void die()
    {
        //사망 애니매이션
        Destroy(gameObject, 1.0f);
    }

}
