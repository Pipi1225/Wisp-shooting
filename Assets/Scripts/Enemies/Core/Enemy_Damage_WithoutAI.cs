using System.Collections;
using UnityEngine;

public class Enemy_Damage_WithoutAI : MonoBehaviour
{
    [Header("Stats:")]
    [SerializeField] private int attackDamage;
    [SerializeField] private float timerExist;
    private SpriteRenderer sprite;
    

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (sprite.size.x <= 1.3f)
        {
            sprite.size += new Vector2(Random.Range(0.003f, 0.006f), 0f);
        }
        Destroy(gameObject, timerExist);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Controller>().TakeDamage(attackDamage, transform.position.x);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Controller>().TakeDamage(attackDamage, transform.position.x);
        }
    }
}
