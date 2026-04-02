using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Rigidbody2D rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rig.constraints = RigidbodyConstraints2D.None;
    }

    public void Stop()
    {
        rig.constraints = RigidbodyConstraints2D.FreezePosition;
    }
}
