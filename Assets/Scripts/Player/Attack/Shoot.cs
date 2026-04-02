using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Stats:")]
    [SerializeField] private bool Cooldown = false;
    [SerializeField] private float shootCooldown;
    [SerializeField] private int count;
    [SerializeField] private int maxCount;
    [SerializeField] private int actualAmount;
    
    [Header("Script:")]
    [SerializeField] private Player_Controller player;
    [SerializeField] private Projectile_Manage pj;

    [Header("UI:")]
    [SerializeField] private ActionBar bulletBar;

    [Header("Controllers:")]
    [SerializeField] private GameObject projectileParent;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<GameObject> projectile;
    [SerializeField] private List<int> prio;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        player = GetComponent<Player_Controller>();
        pj = GameObject.Find("Projectile").GetComponent<Projectile_Manage>();

        bulletBar.SetCoolDown(shootCooldown);
        count = 0;

        startObjectPool();
    }

    void Update()
    {
        /*
        for(var i = projectile.Count - 1; i >= 0; i--)
        {
            if (projectile[i] == null)
            {
                projectile.RemoveAt(i);
            }
        }

        if (count != projectile.Count)
        {
            pj.updateFrame(projectile.Count);
        }

        count = projectile.Count;
        */

        int exist = 0;
        for(int i = 0; i < actualAmount; i++)
        {
            if (projectile[i].activeInHierarchy == false)
            {                
                for (int j = prio.Count - 1; j >= 0; j--)
                {
                    if (prio[j] == i)
                    {
                        prio.RemoveAt(j);
                    }
                }
            }
            else
            {
                exist += 1;
            }
        }

        if (count != exist)
        {
            count = exist;
            pj.updateFrame(exist);
        }
 
        if (Input.GetKeyDown(KeyCode.Mouse1) && !Cooldown && !(player.getHitCooldown()) && !(player.getDeadStatus()))
        {
            Cooldown = true;

            //projectile.Add(Instantiate(bulletPrefab, shootingPoint.position, transform.rotation));
            GameObject obj = getObjectPool();
            obj.SetActive(true);
            obj.transform.position = shootingPoint.position;
            count += 1;

            if (count > maxCount)
            {
                /*
                if (projectile[0] != null)
                {
                    projectile[0].GetComponent<Projectile>().Collided();
                }

                projectile.RemoveAt(0);
                */

                projectile[prio[0]].GetComponent<Projectile>().Collided();
                prio.RemoveAt(0);
            }
            else
            {
                pj.updateFrame(count);
            }

            Invoke("Reset", shootCooldown);
            bulletBar.SetTimer();

            //Create Animation
            anim.SetTrigger("IsCreate");
        }
    }

    public void startObjectPool()
    {
        projectile = new List<GameObject>();
        prio = new List<int>();
        actualAmount = maxCount + 2;

        for (int i = 0; i < actualAmount; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.transform.SetParent(projectileParent.transform);
            obj.SetActive(false);
            projectile.Add(obj);
        }
    }

    public GameObject getObjectPool()
    {
        for (int i = 0; i < actualAmount; i++)
        {
            if (!projectile[i].activeInHierarchy)
            {
                prio.Add(i);
                return(projectile[i]);
            }
        }

        return (null);
    }

    void Reset()
    {
        Cooldown = false;
    }
}
