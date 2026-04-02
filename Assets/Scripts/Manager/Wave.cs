using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [Header("List:")]
    [SerializeField] private List<Transform> place;
    [SerializeField] private int numberPlace;
    [SerializeField] private int numberSky;
    [SerializeField] private int numberGround;

    [Header("Spawn List:")]
    [SerializeField] private List<GameObject> enemyList;
    [SerializeField] private List<int> pointList;
    [SerializeField] private int numberList;

    [Header("Stats:")]
    [SerializeField] private int wavePoint;
    [SerializeField] private int hasPoint;
    [SerializeField] private int waveNumber;
    [SerializeField] private int specials;
    [SerializeField] private WaveCounter UI;

    [Header("Enemy List:")]
    [SerializeField] GameObject enemiesParent;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<int> type;
    [SerializeField] private int activeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        wavePoint = 20;
        hasPoint = wavePoint;
        waveNumber = 1;
        numberList = enemyList.Count;
        numberPlace = place.Count;
        activeEnemy = 0;

        specials = 0;

        UI = GameObject.Find("Wave").GetComponent<WaveCounter>();
    }

    // Update is called once per frame
    /*
    for (int i = numberEnemy - 1; i >= 0; i--)
    {
        /*
        if (enemies[i] == null)
        {
            enemies.RemoveAt(i);
            numberEnemy--;
        }

        if (enemies[i].activeInHierarchy)
        {
            activeEnemy++;
        }

        if (numberEnemy == 0)
        {
            wavePoint += 20 * waveNumber;
            waveNumber++;
            hasPoint = wavePoint;

            if (waveNumber % 5 == 0)
            {
                specials += 1;
            }

            Invoke("Spawn", 2.5f);

            break;
        }
    }
    */

    public void updateActiveEnemy()
    {
        activeEnemy -= 1;

        if (activeEnemy == 0)
        {
            wavePoint += 10 * waveNumber;
            waveNumber++;
            hasPoint = wavePoint;

            if (waveNumber % 5 == 0)
            {
                specials += 1;
            }

            Invoke("Spawn", 2.5f);
        }
    }

    private void EnemySpawning()
    {
        if (hasPoint >= 10)
        {
            int temp = Random.Range(0, numberList - 1);

            if (pointList[temp] <= hasPoint)
            {
                GameObject obj = getObjectPool(temp);
                obj.transform.position = place[Random.Range(0, numberSky)].position;
                obj.SetActive(true);

                //enemies.Add(Instantiate(enemyList[temp], place[Random.Range(0, numberSky)].position, transform.rotation));
                hasPoint -= pointList[temp];
                activeEnemy += 1;
            }

            Invoke("EnemySpawning", 0.1f);
        }
    }

    private void Spawn()
    {
        UI.updateWave(waveNumber);
        Invoke("EnemySpawning", 0.1f);

        for (int i = 0; i < specials; i++)
        {
            GameObject obj = getObjectPool(numberList - 1);
            obj.transform.position = place[Random.Range(numberSky, numberPlace)].position;
            obj.SetActive(true);
        }
        activeEnemy += specials;
    }

    private GameObject getObjectPool(int enemyType)
    {
        int limit = enemies.Count;
        for (int i = 0; i < limit; i++)
        {
            if (!enemies[i].activeInHierarchy && type[i] == enemyType)
            {
                return(enemies[i]);
            }
        }

        GameObject obj = Instantiate(enemyList[enemyType]);
        enemies.Add(obj);
        type.Add(enemyType);
        obj.transform.parent = enemiesParent.transform;
        return(obj);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Invoke("Spawn", 4.5f);
            UI.StartCounting();
        }
    }
}
