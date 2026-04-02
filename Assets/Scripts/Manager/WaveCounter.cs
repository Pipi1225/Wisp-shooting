using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WaveCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text UI;
    [SerializeField] private int counter;
    [SerializeField] private float timeCounter;
    [SerializeField] private float appearTime;
    [SerializeField] private float valuePerSecond;
    [SerializeField] private float alpha;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private bool flag;


    // Start is called before the first frame update
    void Start()
    {
        UI = GetComponent<TMP_Text>();
        Text = GetComponent<TextMeshProUGUI>();
        valuePerSecond = 1 / appearTime;

        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, 0);
    }

    void Update()
    {
        if (appearTime > 0 && flag)
        {
            alpha += Time.deltaTime * valuePerSecond;
            Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, alpha);

            appearTime -= Time.deltaTime;
        }
        
        if (timeCounter > 0 && flag)
        {
            timeCounter -= Time.deltaTime;
            UI.text = Mathf.RoundToInt(timeCounter).ToString();
        }
    }

    public void StartCounting()
    {
        flag = true;
    }

    public void updateWave(int waveNumber)
    {
        counter = waveNumber;
        UI.text = "Wave: " + counter.ToString();
    }
}
