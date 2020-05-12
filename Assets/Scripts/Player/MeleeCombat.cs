using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    public Transform attackPosition;
    public float Damage=1;
    public float attackRange;
    public LayerMask enemyLayer;

    void Start()
    {

    }

    
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies){
            Enemy damagedEnemy = enemy.GetComponent<Enemy>();
            damagedEnemy.TakeDamage(Damage);
        }
    }

    void OnDrawGizmosSelected(){
        if(attackPosition==null) return;
        Gizmos.DrawWireSphere(attackPosition.position,attackRange);
    }
}
