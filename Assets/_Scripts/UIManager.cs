using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Sliders")]
    public Slider timeSlider;
    public Slider happinessSlider;

    private void Awake()
    {
        instance = this;
    }
}
