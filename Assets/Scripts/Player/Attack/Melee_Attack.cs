using System.Collections;
using UnityEngine;

public class Melee_Attack : MonoBehaviour
{
    [Header("Melee Attack:")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange; // 0.17
    [SerializeField] private int attackDamage; // 10
    [SerializeField] private float attackRate; // 1.21
    [SerializeField] private float nextAttackTime = 0f;

    [Header("Air Attack:")]
    [SerializeField] private int attackDamageAir; // 3

    [Header("Explosion Attack:")]
    [SerializeField] private Transform attackPointExplosion;
    [SerializeField] private float attackRangeExplosion; // 0.26
    [SerializeField] private int attackDamageExplosion; // 8

    [Header("Stats:")]
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool grounded;

    [Header("Controllers:")]
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Player_Controller player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        player = GetComponent<Player_Controller>();

        Transform parentTransform = this.transform; 
        attackPoint = parentTransform.Find("AttackPoint");
        attackPointExplosion = parentTransform.Find("AttackPoint_1");
    }

    private void Update()
    {
        isAttacking = Input.GetKeyDown(KeyCode.Z);

        if (Time.time >= nextAttackTime)
        {
            if (isAttacking && anim.GetBool("Ground") && !player.getInmoveableStatus())
            {
                anim.SetTrigger("Melee_Attack");
                nextAttackTime = Time.time + 1f / attackRate;
            } 
            else if (isAttacking && !anim.GetBool("Ground") && !player.getInmoveableStatus() && !anim.GetBool("Air_Attacking"))
            {
                body.velocity = new Vector2(0, -0.2f);
                player.setInmoveableStatus(true);
                anim.SetTrigger("Air_Attack");
                anim.SetBool("Air_Attacking", true);
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies") && anim.GetBool("Air_Attacking"))
        {
            int actualDamage = UnityEngine.Random.Range(attackDamageAir - 1, attackDamageAir + 1);
            collision.GetComponent<Enemy>().EnemyTakeDamage(actualDamage);
            collision.GetComponent<Knockbacked_Enemy>().Knockback();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies") && anim.GetBool("Air_Attacking"))
        {
            Debug.Log(collision);
            int actualDamage = UnityEngine.Random.Range(attackDamageAir - 1, attackDamageAir + 1);
            collision.GetComponent<Enemy>().EnemyTakeDamage(actualDamage);
            collision.GetComponent<Knockbacked_Enemy>().Knockback();
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        int actualDamage = UnityEngine.Random.Range(attackDamage - 2, attackDamage + 1);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().EnemyTakeDamage(actualDamage);

            enemy.GetComponent<Knockbacked_Enemy>().Knockback();
        }
    }

    public void attackExplosion()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointExplosion.position, attackRangeExplosion, enemyLayers);

        int actualDamage = UnityEngine.Random.Range(attackDamageExplosion - 5, attackDamageExplosion + 1);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().EnemyTakeDamage(actualDamage);

            enemy.GetComponent<Knockbacked_Enemy>().Knockback();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPointExplosion.position, attackRangeExplosion);
    }
}
