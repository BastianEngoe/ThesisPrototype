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
    public Slider productionSlider;
    public TMP_Text animalProduction;

    [Header("Instantiation")] [SerializeField]
    private GameObject coinImage;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (!timeSlider)
        {
            timeSlider = GameObject.Find("TimeSlider").GetComponent<Slider>();
        }
        if (!happinessSlider)
        {
            happinessSlider = GameObject.Find("HappinessSlider").GetComponent<Slider>();
        }
        if (!goldText)
        {
            goldText = GameObject.Find("GoldText").GetComponent<TMP_Text>();
        }
        if (!animalLeaveTimer)
        {
            animalLeaveTimer = GameObject.Find("AnimalLeaveTimer").GetComponent<Slider>();
        }
        if (!productionSlider)
        {
            productionSlider = GameObject.Find("ProductivitySlider").GetComponent<Slider>();
        }
        if (!animalProduction)
        {
            animalProduction = GameObject.Find("AnimalProductionText").GetComponent<TMP_Text>();
        }
    }

    private void Update()
    {
        goldText.text = "Gold: " + (int)GameManager.instance.gold;
        happinessSlider.value = GameManager.instance.happiness;
        animalLeaveTimer.value = GameManager.instance.animalLeaveTimer;
        GameManager.instance.produce = productionSlider.value;
        animalProduction.text = "x " + GameManager.instance.animalProduction;
    }
}
