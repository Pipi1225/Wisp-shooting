using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Blop : MonoBehaviour
{
    public Animator anim;
    public Renderer rend;
    public Rigidbody2D body;

    public Knockbacked_Enemy e;

    [Header("Attack:")]
    public Transform attackPoint;

    public bool Enter = false;

    public float attackRange = 0.5f;
    public int attackDamage = 50;

    public float attackRate;
    float nextAttackTime = 0f;

    public LayerMask enemyLayers;
    public bool IsAttacking;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        body = GetComponent<Rigidbody2D>();
        e = GetComponent<Knockbacked_Enemy>();
    }

    private void OnEnable()
    {
        Enter = false;
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Enter && !e.knockbacked)
            {
                anim.SetTrigger("IsAttacking");
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy);
            enemy.gameObject.GetComponent<Player_Controller>().TakeDamage(attackDamage, transform.position.x);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enter = true;
        }
    } 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enter = false;
        }
    }
}
