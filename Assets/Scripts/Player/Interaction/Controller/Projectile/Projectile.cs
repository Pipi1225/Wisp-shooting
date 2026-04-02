using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [Header("Stats:")]
    [SerializeField] private int attackDamage = 20;

    [Header("Controllers:")]
    [SerializeField] private CapsuleCollider2D col;
    [SerializeField] private bool colapse;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer sprite;

    public Vector3 pos { get { return transform.position; } }

    public void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();

        body.constraints = RigidbodyConstraints2D.FreezeAll;       
    }

    private void OnEnable()
    {
        colapse = false;
    }

    public void Push(Vector2 force)
    {
        body.AddForce(force, ForceMode2D.Impulse);
    }

    void Stable()
    {
        anim.SetBool("Stable", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Enemies" || collision.gameObject.tag == "Enemies_B" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall") && !colapse)
        {
            Collided();

            if (collision.gameObject.tag == "Enemies" || collision.gameObject.tag == "Enemies_B")
            {
                collision.GetComponent<Enemy>().EnemyTakeDamage(attackDamage);
                collision.GetComponent<Knockbacked_Enemy>().Knockback();
            }
        }
    }

    public void Collided()
    {
        //bool isCrit = UnityEngine.Random.Range(0, 100) < 30;
        //int actualDamage = UnityEngine.Random.Range(attackDamage - 4, attackDamage + 5);
        
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        colapse = true;
        anim.SetBool("Collision", true);
    }

    public CapsuleCollider2D getCollider()
    {
        return col;
    }

    public bool getColapse()
    {
        return colapse;
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
