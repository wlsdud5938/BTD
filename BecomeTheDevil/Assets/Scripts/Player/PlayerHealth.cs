using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    private UserStatusData userStatus;

    public void Start()
    {
        userStatus = DataManager.Instance.userData_status;
        maxHP = userStatus.GetHealthInfo().getMaxHP();
        currentHP = userStatus.GetHealthInfo().getCurrentHP();
    }

    public new void GetDamage(float attackDamage)
    {
        if (currentHP - attackDamage > 0)
            currentHP = currentHP - DamageCalculator(attackDamage);
        else
        {
            currentHP = 0;
        }

        CharacterPortrait.Instance.SetCurrentHP(currentHP);

        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
