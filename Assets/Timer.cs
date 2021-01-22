using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

using Debug = UnityEngine.Debug;

public class Timer : MonoBehaviour
{

    public Slider slider;

    public Gradient gradient;

    public Image fill;

    public void setMax(float value)
    {
        slider.maxValue = value;
        slider.value = value;

        fill.color = gradient.Evaluate(1);
    }

    public void set(float value)
    {
        slider.value = value;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
