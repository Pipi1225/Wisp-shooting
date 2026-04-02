using System.Collections;
using UnityEngine;

public class Enemy_Damage_Body : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemy.getStatusDead())
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player_Controller>().TakeDamage(attackDamage, transform.position.x);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!enemy.getStatusDead())
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player_Controller>().TakeDamage(attackDamage, transform.position.x);
            }
        }
    }
}
