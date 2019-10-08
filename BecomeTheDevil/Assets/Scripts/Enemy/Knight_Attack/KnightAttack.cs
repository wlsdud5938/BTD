using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAttack : MonoBehaviour
{
    private bool canAttack = true;
    public float attackTimer = 0.1f;

    public Status unitTarget;    // 사거리 안에 들어온 target
    Status stat;
    private Queue<Status> unit = new Queue<Status>();   // target queue
    bool isSearch = false;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = transform.parent.GetComponent<Animator>();
        stat = transform.parent.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        Targetting();
    }

    private void Targetting()
    {
        if (!canAttack) // 공격이 불가능하면
        {
            attackTimer += Time.deltaTime * stat.attackSpeed;
            if(attackTimer >= stat.attackCoolTime)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }

        if(unit.Count > 0 && unitTarget == null)
        {
            unitTarget = unit.Dequeue();
        }

        if(unitTarget != null)
        {
            if (canAttack)
            {
                Attack();
                canAttack = false;
            }
        }
    }

    public void Attack()
    {
        // 검 오브젝트(실제론 안보임) 생성
        GameObject ks = ObjectManager.Call().GetObject("KnightSword");
        ks.SetActive(true);
        Sword sword = ks.GetComponent<Sword>();
        // 검 위치를 Knight 위치로 초기화.
        sword.transform.position = transform.parent.position;
        sword.Initialize(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            unit.Enqueue(other.GetComponent<Status>());
            Debug.Log("unit Enter" + unit.Count);
            //Debug.Log("유닛 들어옴");
            //Debug.Log(unit.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("unit Exit" + unit.Count);
            unitTarget = null;
            //Debug.Log("유닛 나감");
            //Debug.Log(unit.Count);
        }
        //if(other.tag == )
    }
}
