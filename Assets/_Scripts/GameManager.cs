using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Resources")] 
    public float gold;
    [Range(0.0f, 1.0f)] public float happiness;
    public float timer = 600; //10 minutes
    
    [Header("Production")]
    [Range(0.0f, 1.0f)] public float produce;
    public int quota;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        UIManager.instance.timeSlider.value = timer;

        if (happiness > 0.1f)
        {
            gold += 10 * (produce * happiness) * Time.deltaTime;
            happiness -= produce * 0.1f * Time.deltaTime;
        }
    }
}
