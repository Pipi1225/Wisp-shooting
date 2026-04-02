using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Slime : MonoBehaviour
{
    [SerializeField] private int attackDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player_Controller>().TakeDamage(attackDamage, transform.position.x);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
