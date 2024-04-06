using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class EnemyMage : Enemy
{
    [SerializeField] Projectile projectile;
    private float attackTimer = 2f;
    private float currentAttackTiemr = 0f;
    private bool canAttack = true;

    private new void Start()
    {
        base.Start();
        rangeAttack = 10f;
        enemySpeed = 1f;
    }

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

    /*
    override public void goToPlayer()
    {
        
    }
    */

    override public void Attack()
    {

        if (canAttack)
        {
            Projectile newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.setDirection(dirToPlayer);
            newProjectile.setSpeed(3f);
            canAttack = false;
        }
    }
}
