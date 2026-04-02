using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform pfDamagePopup;

    [Header("Durability:")]
    public int maxHealth;
    public int currentHealth;
    public float invicibleTimer;
    [SerializeField] private float offsetStack = 0.3f; // new: 1.2f

    private Animator anim;
    private Renderer rend;
    private Transform body;
    private Rigidbody2D rig;
    [SerializeField] private Collider2D baseCollider;
    [SerializeField] private GameObject drop;
    [SerializeField] private PointParticleManager pointManager;

    private Transform prevdamagePopupTransform;
    private int sumDamage, prevDamage = 0;
    private float time;
    private float ttime;

    [Header("Status:")]
    [SerializeField] private bool dead = false;
    [SerializeField] private int point;
    [SerializeField] private Wave waveManager;
    public bool hitCooldown = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        body = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();

        pointManager = GameObject.Find("Point Particle").GetComponent<PointParticleManager>();
        waveManager = GameObject.Find("Wave Spawn").GetComponent<Wave>();

        time = 0;
    }

    private void OnEnable()
    {
        time = 0;
        currentHealth = maxHealth;
        hitCooldown = false;
        dead = false;
    }

    private void OnDisable()
    {
        SetAllCollidersStatus();
    }

    public bool getStatusDead()
    {
        return(dead);
    }

    public void setStatusDead()
    {
        waveManager.updateActiveEnemy();
    }

    public void EnemyTakeDamage(int damage)
    {
        time = Time.time;
        
        if (hitCooldown == false)
        {    
            hitCooldown = true;    
            damagePopup damagePopup;

            sumDamage = damage + prevDamage;
            prevDamage = sumDamage;

            if (dead)
            {
                return;
            }

            if ((time - ttime) <= offsetStack && prevdamagePopupTransform != null) 
            {
                damagePopup = prevdamagePopupTransform.GetComponent<damagePopup>();

                damagePopup.Setup(sumDamage, body.position);
            } 
            else 
            {
                Transform damagePopupTransform = Instantiate(pfDamagePopup, body.position, Quaternion.identity);
                prevdamagePopupTransform = damagePopupTransform;

                damagePopup = damagePopupTransform.GetComponent<damagePopup>();
                
                damagePopup.Setup(damage, body.position);
                prevDamage = damage;
            }

            ttime = time;
            anim.SetTrigger("Hurt");

            currentHealth -= damage;
            
            if (currentHealth <= 0)
            {
                Debug.Log("Enemy die!");

                SetAllCollidersStatus();
                anim.SetBool("IsDead", true);
                rig.gravityScale = 1;

                Invoke("Drop", 0.5f);
                Invoke("Die", 3f);
            }

            Invoke("Cooldown", invicibleTimer);
        }        
    }

    void Die()
    {
        setStatusDead();
        gameObject.SetActive(false);
    }

    void Drop()
    {
        int chance = Random.Range(1, 101);
        if (1 <= chance && chance <= 20)
        {
            Instantiate(drop, body.position, Quaternion.identity);
        }

        GameObject obj = pointManager.getObjectPool();
        obj.SetActive(true);
        obj.transform.position = transform.position;
        if (point > 0)
        {
            obj.GetComponent<Particle_Manager>().setPoint(point);
        }
    }
    
    public void Cooldown()
    {
        hitCooldown = false;
    }

    public void SetAllCollidersStatus()
    {
        foreach(Collider2D collider in GetComponentsInChildren<Collider2D>()) {
            if (collider != baseCollider)
            {
                collider.enabled = !collider.enabled;
            }
        }
    }
}