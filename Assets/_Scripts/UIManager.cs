using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Resources")]
    public Slider timeSlider;
    public Slider happinessSlider;
    public TMP_Text goldText;
    public Slider animalLeaveTimer;

    [Header("UI Elements")] 
    public GameObject gamblingPanel;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        goldText.text = "Gold: " + (int)GameManager.instance.gold;
        happinessSlider.value = GameManager.instance.happiness;
        animalLeaveTimer.value = GameManager.instance.animalLeaveTimer;
    }
}
