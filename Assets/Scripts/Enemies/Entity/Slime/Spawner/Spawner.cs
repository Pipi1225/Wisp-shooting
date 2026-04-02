using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemy List:")]
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Transform place;

    [Header("Stats:")]
    [SerializeField] public bool Cooldown;
    [SerializeField] private Monster_Movement movement;

    private void Awake()
    {
        Cooldown = false;
    }

    public bool checkSpawn()
    {
        if (getObjectPool() == null)
        {
            return false;
        }

        return true;
    }

    private void EnemySpawning()
    {
        GameObject obj = getObjectPool();

        if (obj != null && Cooldown == false)
        {
            obj.transform.position = place.position;
            obj.SetActive(true);

            float forceX = Mathf.Sign(movement.distanceFromPlayer) * Mathf.Min(Mathf.Abs(movement.distanceFromPlayer), 2.5f) * 1.18f;
            float forceY = 4 * (1 + Mathf.Abs(movement.distanceFromPlayer) / 20);

            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(forceX, forceY);
            Cooldown = true;     
            Invoke("Reset", 20f);
        }
    }

    private GameObject getObjectPool()
    {
        int limit = enemies.Count;
        for (int i = 0; i < limit; i++)
        {
            if (!enemies[i].activeInHierarchy)
            {
                return(enemies[i]);
            }
        }

        return(null);
    }

    private void Reset()
    {
        Cooldown = !Cooldown;
    }
}
