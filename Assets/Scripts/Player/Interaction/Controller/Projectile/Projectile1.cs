using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    private Projectile projectile;
    public Transform attackPoint;

    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    private int actualDamage;

    public LayerMask enemyLayers;
    public void Awake()
    {
        projectile = GetComponent<Projectile>();
    }

    public void Explode()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (projectile.getColapse())
        {
            actualDamage = UnityEngine.Random.Range(attackDamage - 3, attackDamage + 4);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().EnemyTakeDamage(actualDamage);
                enemy.GetComponent<Knockbacked_Enemy>().Knockback();
            }
        }
            
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
