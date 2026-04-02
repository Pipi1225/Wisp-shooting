using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject particleParent;
    [SerializeField] private GameObject particleSystemPrefab;
    [SerializeField] private List<GameObject> particles;
    [SerializeField] private int limit;

    // Start is called before the first frame update
    void Start()
    {
        particleParent = gameObject;
        limit = 0;
    }

    public GameObject getObjectPool()
    {
        for (int i = 0; i < limit; i++)
        {
            if (!particles[i].activeInHierarchy)
            {
                return(particles[i]);
            }
        }

        GameObject obj = Instantiate(particleSystemPrefab);
        obj.transform.SetParent(particleParent.transform);
        obj.SetActive(false);
        particles.Add(obj);
        limit += 1;

        return(obj);
    }
}
