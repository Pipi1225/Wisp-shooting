using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockbacked_Enemy : MonoBehaviour
{
    [Header("Knockback stats:")]
    [SerializeField] private float knockBackForceUp;
    [SerializeField] private float knockBackForce;

    [Header("Status:")]
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer renderer;
    public bool knockbacked;
    public float knockbackSec;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Knockback()
    {
        if (anim.GetBool("IsDead"))
        {
            return;
        }

        Transform attacker = getClosestDamageSource();

        knockbacked = true;

        float posX = Mathf.Abs(attacker.transform.position.x - transform.position.x), posY = Mathf.Abs(attacker.transform.position.y - transform.position.y);
        float M = posX / posY;
        
        float knockBackDirectorY = 1 / (M + 1);
        float knockBackDirectorX = 1 - knockBackDirectorY;

        if (attacker.transform.position.y - transform.position.y > 0.4)
        {
            knockBackDirectorY = Mathf.Min(knockBackForceUp * -1, knockBackDirectorY * -1);
        }
        else
        {
            knockBackDirectorY = Mathf.Max(knockBackForceUp, knockBackDirectorY);
        }

        if (attacker.transform.position.x > transform.position.x)
        {
            knockBackDirectorX *= -1;
        }

        body.velocity = new Vector2(knockBackDirectorX, knockBackDirectorY) * knockBackForce;

        //Vector3 t = transform.localScale;

        if (knockBackDirectorX < 0.01f)
        {
            /*
            if (t.x > 0) 
            {
                t.x *= -1;
            }
            */
            renderer.flipX = true;

            //transform.localScale = t;
        }
        else if (knockBackDirectorX > -0.01f)
        {
            /*
            if (t.x < 0) 
            {
                t.x *= -1;
            }
            */
            renderer.flipX = false;

            //transform.localScale = t;
        }

        Invoke("Cooldown", knockbackSec);
    }

    private Transform getClosestDamageSource()
    {
        GameObject[] DamageSources = GameObject.FindGameObjectsWithTag("Weapon");
        float closestDistanz = Mathf.Infinity;
        Transform currentClosestDamageSource = null;

        foreach (GameObject go in DamageSources)
        {
            float currentDistanz;
            currentDistanz = Vector3.Distance(transform.position, go.transform.position);

            if (currentDistanz < closestDistanz)
            {
                closestDistanz = currentDistanz;
                currentClosestDamageSource = go.transform;
            }
        }

        return currentClosestDamageSource;
    }

    private void Cooldown()
    {
        knockbacked = false;
    }
}
