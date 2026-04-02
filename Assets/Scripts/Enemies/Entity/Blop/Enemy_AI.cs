using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI : MonoBehaviour
{
    public GameObject player;
    public Transform enemyGFX;
    public Animator anim;
    private Seeker seeker;
    private Rigidbody2D body;
    private Transform target;
    private Enemy health;

    public Knockbacked_Enemy k;

    public bool Enter = false;

    [SerializeField] float speed;
    [SerializeField] float s;
    [SerializeField] [Range(1f, 5f)] float nextWaypointDistance;
    [SerializeField] Vector3 offset;
    [SerializeField] bool changing;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    // Start is called before the first frame update
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        k = GetComponent<Knockbacked_Enemy>();
        health = GetComponent<Enemy>();

        player = GameObject.Find("Player");
        target = player.transform;

        InvokeRepeating("UpdatePath", 0f, 0.45f);
    }

    private void OnDisable()
    {
        body.gravityScale = 0f;
    }

    private void UpdatePath()
    {
        Vector3 targetPost = target.position + offset; 

        if (seeker.IsDone())
        {
            seeker.StartPath(body.position, targetPost, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    private void Update()
    {    
        if (health.currentHealth <= 0)
        {
            body.velocity = new Vector2(0, body.velocity.y);
            return;
        }

        if (k.knockbacked)
        {
            return;
        }

        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        speed = Mathf.Min(s * (1 + 2 * (Vector2.Distance(body.position, target.position))), 150f);
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (changing)
        {
            float angle = Random.Range(-20f, 20f) * Mathf.Deg2Rad;
            force = new Vector2(force.x * Mathf.Cos(angle) - force.y * Mathf.Sin(angle), force.x * Mathf.Sin(angle) + force.y * Mathf.Cos(angle));
        }

        body.velocity = force;

        float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }   
    }
}