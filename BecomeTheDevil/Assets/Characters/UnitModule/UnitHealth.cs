using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : Health
{
    AnimationControlScript ani;

    void Start()
    {
        currentHP = maxHP;
        ani = GetComponent<AnimationControlScript>();
    }

    public override void GetDamage(float attackDamage)
    {
        if (currentHP - attackDamage > 0)
            currentHP = currentHP - DamageCalculator(attackDamage);
        else
        {
            currentHP = 0;
            isDie = true;
        }

        if (!isDie && currentHP <= 0)
        {
            isDie = true;
            ani.Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (!gameObject.CompareTag("Player") && (other.CompareTag("EnemyBullet") && (!gameObject.CompareTag("Enemy") && !gameObject.CompareTag("Field")))
            || (other.CompareTag("FriendlyBullet") && (!gameObject.CompareTag("Unit"))))
            GetDamage(other.GetComponent<Bullet>().damage);
    }
}
