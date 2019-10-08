using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public float objAggroStack;
    public GameObject curRoom;
    public float maxHP;
    public float curHP;

    public float attackDMG;
    public float attackSpeed;
    public float attackCoolTime;
    public float attackRange;

    // Start is called before the first frame update
    void Start()
    {
        curHP = 100f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        curHP -= damage;
    }
}
