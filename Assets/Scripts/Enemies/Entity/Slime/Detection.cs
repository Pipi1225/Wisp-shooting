using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public Monster_Movement script;

    void Awake()
    {
        script.col = true;
    }
}
