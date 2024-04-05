using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public float animalLeaveTimer = 30f;

    [Header("Animals")]
    public GameObject[] animals;
    public List<GameObject> boughtAnimals;
    [SerializeField] private BoxCollider2D animalPenCollider;
    
    [Header("Cost of Animals")]
    public float costOfChicken = 10;
    public float costOfSheep = 100;
    public float costOfPig = 400;
    public float costOfCow = 2000;

    [Header("Gambling")] 
    public bool hasGambledOnce;
    public TMP_InputField gambleInput;


    private void Awake()
    {
        instance = this;
    }
    
    
    private void Update()
    {
        timer -= Time.deltaTime;

        if (UIManager.instance) UIManager.instance.timeSlider.value = timer;

        if (happiness > 0.01f)
        {
            gold += animalProduction * (produce * happiness) * Time.deltaTime;
            happiness -= produce * 0.1f * Time.deltaTime;
            UIManager.instance.animalLeaveTimer.gameObject.SetActive(false);
            animalLeaveTimer = 30f;
        }
        else if (boughtAnimals.Count > 0)
        {
            UIManager.instance.animalLeaveTimer.gameObject.SetActive(true);
            animalLeaveTimer -= Time.deltaTime;
            if (animalLeaveTimer < 0)
            {
                LeaveAnimal();
                animalLeaveTimer = 30f;
            }
        }
    }

    public bool BuyAnimal(string animal)
    {
        bool boughtSuccessful = false;
        GameObject animalToBuy;

        if (animal == "Chicken" && gold >= 10)
        {
            gold -= costOfChicken;
            if (animals[0] != null)
            {
                animalToBuy = Instantiate(animals[0], RandomPenLocation(), Quaternion.Euler(0, 130, 0));
                boughtAnimals.Add(animalToBuy);
                animalProduction += animalToBuy.GetComponent<Animal>().production;
            }
            boughtSuccessful = true;
        }

        if (animal == "Sheep" && gold >= 100)
        {
            gold -= costOfSheep;
            if (animals[1] != null)
            {
                animalToBuy = Instantiate(animals[1], RandomPenLocation(), Quaternion.Euler(0, 130, 0));
                boughtAnimals.Add(animalToBuy);
                animalProduction += animalToBuy.GetComponent<Animal>().production;
            }
            boughtSuccessful = true;
        }

        if (animal == "Pig" && gold >= 400)
        {
            gold -= costOfPig;
            if (animals[2] != null)
            {
                animalToBuy = Instantiate(animals[2], RandomPenLocation(), Quaternion.Euler(0, 130, 0));
                boughtAnimals.Add(animalToBuy);
                animalProduction += animalToBuy.GetComponent<Animal>().production;
            }
            boughtSuccessful = true;
        }

        if (animal == "Cow" && gold >= 1000)
        {
            gold -= costOfCow;
            if (animals[3] != null)
            {
                animalToBuy = Instantiate(animals[3], RandomPenLocation(), Quaternion.Euler(0, 130, 0));
                boughtAnimals.Add(animalToBuy);
                animalProduction += animalToBuy.GetComponent<Animal>().production;
            }
            boughtSuccessful = true;
        }

        return boughtSuccessful;
    }

    private Vector3 RandomPenLocation()
    {
        float minX = animalPenCollider.bounds.min.x;
        float minY = animalPenCollider.bounds.min.y;
        float maxX = animalPenCollider.bounds.max.x;
        float maxY = animalPenCollider.bounds.max.y;

        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), -1);

        return randomPosition;
    }

    private void LeaveAnimal()
    {
        GameObject animalToLeave = boughtAnimals[Random.Range(0, boughtAnimals.Count)];
        Animator animalAnim = animalToLeave.GetComponent<Animator>();
        boughtAnimals.Remove(animalToLeave);
        
        int whichAnim = Random.Range(0, 3);
        switch (whichAnim)
        {
            case 0: animalAnim.Play("Walk");
                break;
            case 1: animalAnim.Play("Run");
                break;
            case 2: animalAnim.Play("Spin");
                break;
        }

        animalProduction -= animalToLeave.GetComponent<Animal>().production;
        //animalToLeave.transform.DOMoveX(animalToLeave.transform.position.x + 5f, 3f);
        animalToLeave.transform.DOMove(
            new Vector3(animalToLeave.transform.position.x + 5f, animalToLeave.transform.position.y,
                animalToLeave.transform.position.z - 11), 3f);
        Destroy(animalToLeave, 3f);
    }

    public void Gamble()
    {
        if (gambleInput.text == "")
        {
            Debug.Log("Please enter a value to gamble");
            return;
        } 
        
        if (int.Parse(gambleInput.text) <= (int)gold)
        {
            if (!hasGambledOnce)
            {
                hasGambledOnce = true;
                gold -= int.Parse(gambleInput.text);
                Debug.Log("Gamble lost!");
            }
            else
            {
                int chance = Random.Range(0, 100);
                if (chance < 50)
                {
                    gold -= int.Parse(gambleInput.text);
                    Debug.Log("Gamble lost!");
                }
                else
                {
                    gold += int.Parse(gambleInput.text);
                    Debug.Log("Gamble won!");
                }
            }
        }
        else
        {
            Debug.Log("You don't have enough gold to gamble that amount");
        }
    }
}
