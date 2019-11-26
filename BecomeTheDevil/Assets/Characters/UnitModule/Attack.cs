using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool isTargeting;
    float attackRange = 6;
    float attackDamage = 10;
    float attackCooldown = 1;
    bool canMoveingAttack = false;
    int attackCount = 1;
    float bulletSpeed = 6;
    float bulletAcceleration = 6;
    bool isKnockback = false;
    float knockbackPower = 1;

    public GameObject target;
    FindAggroTarget aggroTarget;
    // Start is called before the first frame update
    void Start()
    {
        aggroTarget = GetComponent<FindAggroTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        target = aggroTarget.target;
    }

}
