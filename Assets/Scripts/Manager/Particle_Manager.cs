using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Particle_Manager : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Point UI;
    [SerializeField] private List<ParticleSystem.Particle> particles;
    [SerializeField] private float timeExist;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.trigger.SetCollider(0, GameObject.Find("Player").transform);

        UI = GameObject.Find("Point").GetComponent<Point>();
        particles = new List<ParticleSystem.Particle>();
    }

    public void setPoint(int point)
    {
        Invoke("stopExist", timeExist);

        int numberEmit = point / 5;
        ps.Emit(numberEmit);
    }

    private void stopExist()
    {
        gameObject.SetActive(false);
    }

    private void OnParticleTrigger()
    {
        int limit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);

        for (int i = 0; i < limit; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            UI.updatePoint(5);
            Debug.Log("+5");

            particles[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
