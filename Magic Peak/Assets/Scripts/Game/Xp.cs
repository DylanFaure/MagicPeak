using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Xp : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxXp(float xp)
    {
        slider.maxValue = xp;
        slider.value = 0;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetXp(float xp)
    {
        slider.value = xp;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
