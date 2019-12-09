using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool isTargeting;
    public float attackRange = 6;
    public float attackDamage = 10;
    public float attackCooldown = 1;
    public bool canMoveingAttack = false;
    public int attackCount = 1;
    public  float bulletSpeed = 6;
    public float bulletAcceleration = 6;
    public bool isKnockback = false;
    public float knockbackPower = 1;
    bool canAttack = false;
    public GameObject target;
    FindAggroTarget aggroTarget;

    public bool isChargeUnit = false;
    public float chargeTime = 0f;

    public Vector3 targetPos;
    Animator animator;
    public Vector3 pos;
    public bool targetIn = false;
    //Bullet
    float maxDis = 6;
    float maxSpeed = 6;

    float setDis = 0.5f;

    public Transform attackPosition;
    // Start is called before the first frame update
    void Start()
    {
        aggroTarget = GetComponent<FindAggroTarget>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name != "Enemy_01_knight(Clone)")
        {
            IsCanAttack();
            if (!gameObject.CompareTag("Player") && aggroTarget.hasTarget)
            {
                target = aggroTarget.target;
                if (canAttack && targetIn)
                {
                    animator.SetBool("Attack", true);
                    if(isChargeUnit)
                        animator.SetBool("Charge", true);

                }

            }
        }
        if (!gameObject.CompareTag("Player"))
        {
            target = aggroTarget.target;
            if (target == null || target.activeSelf == false)
                animator.SetBool("Attack", false);
        }


    }

    //공격 쿨타임에 따른 공격 가능 여부 확인
    void IsCanAttack()
    {
        attackCooldown += Time.deltaTime;
        if (attackCooldown >= 1)
            canAttack = true;
        else
        {
            canAttack = false;
            animator.SetBool("Attack", false);

        }
    }
    public void CloseAreaAttack()
    {
        if (canAttack)
        {
            attackCooldown = 0;
            for (int i = 0; i < aggroTarget.enemyList.Count; i++)
            {
                aggroTarget.enemyList[i].GetComponent<Health>().GetDamage(attackDamage);
            }
        }
    }
    public void CloseAttack()
    {
        attackCooldown = 0;
        target.GetComponent<Health>().GetDamage(attackDamage);
    }
    public void EndAttack()
    {
        attackCooldown = 0;
    }
    public void RangedAttack()
    {
            if (canAttack && target != null && target.activeSelf)
                StartCoroutine("BulletAttack");
    }

    void BulletInfoSetting(GameObject _Bullet)
    {
        if (_Bullet == null) return;
        if (attackPosition == null)
        {
            targetPos = new Vector3(target.transform.position.x - transform.position.x, 0, target.transform.position.z - transform.position.z);
            pos = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else
        {
            targetPos = new Vector3(target.transform.position.x - attackPosition.position.x, 0, target.transform.position.z - attackPosition.position.z);
            pos = new Vector3(attackPosition.position.x, 0, attackPosition.position.z);

        }


        _Bullet.SetActive(true);
        _Bullet.transform.position = pos + targetPos.normalized * setDis;

        _Bullet.GetComponent<Bullet>().damage = attackDamage;
        _Bullet.GetComponent<Bullet>().maxSpeed = maxSpeed;
        _Bullet.GetComponent<Bullet>().target = targetPos;
        _Bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        _Bullet.GetComponent<Bullet>().maxDis = maxDis;
        _Bullet.GetComponent<Bullet>().chargeTime = chargeTime;
        _Bullet.GetComponent<Bullet>().StartCoroutine("MoveBullet");
    }

    IEnumerator BulletAttack()
    {
        canAttack = false;
        for (int i = 0; i < attackCount; i++)
        {
            if (gameObject.CompareTag("Unit") || gameObject.CompareTag("Player"))
                BulletInfoSetting(ObjectManager.Call().GetObject("GreenBullet"));
            else
                BulletInfoSetting(ObjectManager.Call().GetObject("EnemyBullet"));
            if (isChargeUnit)
            {
                animator.SetBool("Charge", true);
                Debug.Log("chargeb");
                yield return new WaitForSeconds(chargeTime);
                Debug.Log("chargef");
                animator.SetBool("Charge", false);
                animator.SetBool("ChargeDone", true);

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
