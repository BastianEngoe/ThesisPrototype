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
    
    [Header("Cost of Animals")]
    public float costOfChicken = 10;
    public float costOfSheep = 100;
    public float costOfPig = 400;
    public float costOfCow = 2000;

    private void Awake()
    {
        instance = this;
    }
    
    

    private void Update()
    {
        timer -= Time.deltaTime;

        if (UIManager.instance) UIManager.instance.timeSlider.value = timer;

        if (happiness > 0.1f)
        {
            gold += animalProduction * (produce * happiness) * Time.deltaTime;
            happiness -= produce * 0.1f * Time.deltaTime;
        }
    }

    public bool BuyAnimal(string animal)
    {
        bool boughtSuccessful = false;

        if (animal == "Chicken" && gold >= 10)
        {
            animalProduction += 0.1f;
            gold -= costOfChicken;
            if (animals[0] != null)
            {
                Instantiate(animals[0], animalPen.position, quaternion.identity);
            }
            boughtSuccessful = true;
        }

        if (animal == "Sheep" && gold >= 100)
        {
            animalProduction += 0.2f;
            gold -= costOfSheep;
            if (animals[1] != null)
            {
                Instantiate(animals[1], animalPen.position, quaternion.identity);
            }
            boughtSuccessful = true;
        }

        if (animal == "Pig" && gold >= 400)
        {
            animalProduction += 0.5f;
            gold -= costOfPig;
            
            if (animals[2] != null)
            {
                Instantiate(animals[2], animalPen.position, quaternion.identity);
            }
            boughtSuccessful = true;
        }

        if (animal == "Cow" && gold >= 1000)
        {
            animalProduction += 1.25f;
            gold -= costOfCow;
            if (animals[3] != null)
            {
                Instantiate(animals[3], animalPen.position, quaternion.identity);
            }
            boughtSuccessful = true;
        }

        return boughtSuccessful;
    }
}
