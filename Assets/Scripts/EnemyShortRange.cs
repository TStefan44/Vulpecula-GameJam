using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShortRange : Enemy
{
    private float attackTimer = 2f;
    private float currentAttackTiemr = 0f;
    private bool canAttack = true;

    private new void Update()
    {
        base.Update();
        if (canAttack == true)
        {
            return;
        }

        if (currentAttackTiemr < attackTimer)
        {
            currentAttackTiemr += Time.deltaTime;
            return;
        }
        else
        {
            currentAttackTiemr = 0f;
            canAttack = true;
        }
    }

    override public void Attack() {
        base.Attack();

        if (canAttack)
        {
            animator.SetTrigger("isAttack");
            audioManager.PlaySFX(audioManager.enemyAttack);
            canAttack = false;
        }
    }
}
