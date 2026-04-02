using System.Collections;
using UnityEngine;

public class Monster_Movement : MonoBehaviour
{
    [Header("Stats:")]
    [SerializeField] private float speed;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] public float distanceFromPlayer;
    [SerializeField] public Spawner spawner;
    private float g;

    [Header("Status:")]
    [SerializeField] private bool flip;
    [SerializeField] private bool grounded;
    [SerializeField] private bool shooter;
    public bool col;
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer renderer;

    public Knockbacked_Enemy script;

    [Header("Jump Attacking:")]
    public float jumpHeight;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        
        g = Mathf.Abs(Physics2D.gravity.y);
        Player = GameObject.Find("Player");
    }

    private void OnEnable()
    {
        //body.gravityScale = 1f;
    }

    private void Update()
    {
        if (grounded && col)
        {
            //Flip towards player
            //Vector3 scale = transform.localScale;

            if (!script.knockbacked)
            {
                if (Player.transform.position.x > transform.position.x)
                {
                    renderer.flipX = flip ? false : true;
                    //scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
                }
                else
                {
                    renderer.flipX = flip ? true : false;
                    //scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
                }
                //transform.localScale = scale; 
            }
            
            distanceFromPlayer = Player.transform.position.x - transform.position.x;

            if (shooter)
            {
                if (spawner != null && spawner.Cooldown == false && spawner.checkSpawn())
                {
                    if (0.5f <= Mathf.Abs(distanceFromPlayer) && Mathf.Abs(distanceFromPlayer) <= 2.5f)
                    {
                        anim.SetBool("InRange", true);
                    }
                    else
                    {
                        anim.SetBool("InRange", false);
                    }
                }
                else if (spawner == null)
                {
                    if (0.5f <= Mathf.Abs(distanceFromPlayer) && Mathf.Abs(distanceFromPlayer) <= 2.5f)
                    {
                        anim.SetBool("InRange", true);
                    }
                    else
                    {
                        anim.SetBool("InRange", false);
                    }
                }
            }
        }        
    }

    private void JumpAttack()
    {
        if (grounded && col)
        {
            grounded = false;
            
            float dX = Mathf.Clamp(distanceFromPlayer, -1.5f, 1.5f);
            //if (distanceFromPlayer > 0) 
            //{
            //    dX = Mathf.Min(distanceFromPlayer,  1.5f);

            //    dX += Random.Range(0.0f, 0.2f);
            //} 
            //else 
            //{
            //    dX = Mathf.Max(distanceFromPlayer, -1.5f);

            //    dX -= Random.Range(0.0f, 0.2f);
            //}

            if (Random.value < 0.1f)
            {
                dX = dX * 1.25f;
            }
            
            //float yMax = Mathf.Max(Player.transform.position.y, transform.position.y) + jumpHeight * (1 + Mathf.Abs(dX) / 50);
            float yMax = transform.position.y + jumpHeight * (1 + Mathf.Abs(dX) / 10);
            float forceY = Mathf.Sqrt(2 * g * Mathf.Abs(yMax - transform.position.y));

            float tUp = forceY / g;
            float tDown = Mathf.Sqrt(2 * Mathf.Abs(yMax - Player.transform.position.y) / g);
            float totalTime = tUp + tDown;

            float forceX = dX / totalTime;
            forceX = Mathf.Clamp(forceX, -3f, 3f);

            body.velocity = new Vector2(forceX, forceY);
        }
    }

    private void ShootAttack()
    {
        if (grounded)
        {
            float forceX = Mathf.Sign(distanceFromPlayer) * Mathf.Min(Mathf.Abs(distanceFromPlayer), 2.5f) * 1.18f;
            float forceY = 4 * (1 + Mathf.Abs(distanceFromPlayer) / 20);

            Instantiate(projectile, pos1.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2( forceX, forceY);
            Instantiate(projectile, pos2.position, transform.rotation).GetComponent<Rigidbody2D>().velocity = new Vector2(-forceX, forceY);
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

    private void ResetBool()
    {
        anim.SetBool("InRange", false);
    }
}
