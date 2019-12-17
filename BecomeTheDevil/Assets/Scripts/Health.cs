using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float maxHP = 100;       //최대 체력(현재 체력 상한)
    public float currentHP;         //현재 체력
    public float defensive =0f;     //방어력 (가중치가 1일때 0~1의 값)
    public int durability;          //내구도

    [HideInInspector]
    public float defWeight = 1f;           // 방어력 가중치(가중치가 작으면 낮은 방어력 기울기 낮음)
    [HideInInspector]
    public bool isDie = false;
    public abstract void GetDamage(float attackDamage);

    public float DamageCalculator(float damage)
    {
        if(damage - (1 - Mathf.Sqrt(defWeight * defensive)) >= 0)
            return damage * (1 - Mathf.Sqrt(defWeight * defensive));
        return 0;
    }
}
