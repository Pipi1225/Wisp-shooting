using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_Enemy : MonoBehaviour
{
    [Header("Stats:")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject Player;

    [Header("Status:")]
    [SerializeField] private bool flip;
    [SerializeField] private bool grounded;
    public bool col;
    private Rigidbody2D body;
    private Animator anim;

    public Knockbacked_Enemy script;

    [Header("Jump Attacking:")]
    public float jumpHeight;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        Player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (grounded && col)
        {
            //Flip towards player
            Vector3 scale = transform.localScale;

            if (!script.knockbacked)
            {
                if (Player.transform.position.x > transform.position.x)
                {
                    scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                }
                else
                {
                    scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
                } 

                transform.localScale = scale; 
            }
        }        
    }

    private void Attack()
    {
        float distancefromPlayer = (Player.transform.position.x - transform.position.x) * (1.06f);

        if (distancefromPlayer > 0) 
        {
            distancefromPlayer = Mathf.Min(distancefromPlayer, 1.5f);

            distancefromPlayer += Random.Range(0.0f, 0.2f);
        } 
        else 
        {
            distancefromPlayer = Mathf.Max(distancefromPlayer, -1.5f);

            distancefromPlayer -= Random.Range(0.0f, 0.2f);
        }

        if (grounded && col)
        {
            //Debug.Log(distancefromPlayer);
            body.velocity = new Vector2(distancefromPlayer, jumpHeight * (1 + Mathf.Abs(distancefromPlayer) / 50));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check grounded
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    } 
}
