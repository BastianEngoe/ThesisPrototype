using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    public float animalProduction = 1;

    [Header("Animals")] 
    public Transform animalPen;
    public GameObject[] animals;

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
            gold += animalProduction * (produce * happiness) * Time.deltaTime;
            happiness -= produce * 0.1f * Time.deltaTime;
        }
    }

    public void BuyAnimal(string animal)
    {
        if (animal == "Hen")
        {
            if (gold < 50) return;
            animalProduction += 0.1f;
            gold -= 50;
            Instantiate(animals[0], animalPen.position, quaternion.identity);
        }

        if (animal == "Sheep")
        {
            if(gold < 75 ) return;
            animalProduction += 0.2f;
            gold -= 75;
            Instantiate(animals[1], animalPen.position, quaternion.identity);
        }

        if (animal == "Pig")
        {
            if (gold < 150) return;
            animalProduction += 0.5f;
            gold -= 150;
            Instantiate(animals[2], animalPen.position, quaternion.identity);
        }

        if (animal == "Cow")
        {
            if (gold < 300) return;
            animalProduction += 1.25f;
            gold -= 300;
            Instantiate(animals[3], animalPen.position, quaternion.identity);
        }
    }
}
