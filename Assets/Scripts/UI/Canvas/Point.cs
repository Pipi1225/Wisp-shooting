using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Point : MonoBehaviour
{
    [SerializeField] private TMP_Text UI;
    [SerializeField] private int point;

    // Start is called before the first frame update
    void Start()
    {
        UI = GetComponent<TMP_Text>();
        point = 0;
    }

    public void updatePoint(int value)
    {
        point += value;
        UI.text = point.ToString();
    }
}
