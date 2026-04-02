using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Blight : MonoBehaviour
{
    [Header("Status:")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private bool inRange;
    [SerializeField] private GameObject prefab;

    private Rigidbody2D body;
    private Animator anim;
    private Enemy enemy;

    private void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();

        inRange = false;

        player = GameObject.Find("Player");
        target = player.transform;
    }

    private void OnEnable()
    {
        inRange = false;
    }

    private void Moving()
    {
        float distancefromPlayer = (target.transform.position.x - transform.position.x);

        if (!inRange && Mathf.Abs(distancefromPlayer) <= radius)
        {
            anim.SetBool("Spawning", true);
            inRange = true;
        }

        if (!inRange && !enemy.getStatusDead())
        {
            Vector2 temp = body.velocity;

            if (distancefromPlayer > 0)
            {
                temp.x = speed;
            }
            else
            {
                temp.x = -speed;
            }

            temp.x += Random.Range(0f, 0.1f);

            body.velocity = temp;
        }
    }

    public void Spawn()
    {
        Vector2 temp = transform.position;
        temp.y -= 0.2f;

        enemy.setStatusDead();
        anim.SetBool("IsDead", true);
        Instantiate(prefab, temp, transform.rotation);
        gameObject.SetActive(false);
    }
}
