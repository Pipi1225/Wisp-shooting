using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    void Update()
    {
        if (slider.value > 0)
        {
            slider.value -= Time.deltaTime;
        }
    }

    public void SetCoolDown(float time)
    {
        slider.maxValue = time;
    }

    public void SetTimer()
    {
        slider.value = slider.maxValue;
    }
}
