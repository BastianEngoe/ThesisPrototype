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

    [Header("Group")] public bool isGroupA = false;
    
    [Header("Resources")] 
    public float gold;
    [Range(0.0f, 1.0f)] public float happiness;
    public float timer = 720; //12 minutes
    public float elapsedTime = 0;
    public static event Action MoneyJustIncreased; // Declare events
    public static event Action GameOver;
    
    public float previousGold;

    [Header("Production")]
    [Range(0.0f, 1.0f)] public float produce;
    public int quota;
    public float animalProduction = 1;
    public float animalLeaveTimer = 30f;
    public float minigameTimer = 20f;

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

    [Header("Happiness Minigames")] 
    [SerializeField] private GameObject[] minigamePrefabs;


    private void Start()
    {
        previousGold = gold - (gold/2);
    }

    private void Awake()
    {
        instance = this;
        
        GameObject animalToBuy = Instantiate(animals[0], RandomPenLocation(), Quaternion.Euler(0, 130, 0));
        boughtAnimals.Add(animalToBuy);
        animalProduction += animalToBuy.GetComponent<Animal>().production;
    }
    
    
    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (UIManager.instance) UIManager.instance.timeSlider.value = elapsedTime;

        if (happiness < 0.99f)
        {
            float depression = 1 - happiness;
            gold += animalProduction * (produce * depression) * Time.deltaTime;
            happiness += produce * 0.05f * Time.deltaTime;
            UIManager.instance.animalLeaveTimer.gameObject.SetActive(false);
            animalLeaveTimer = 30f;
        }
        else if(boughtAnimals.Count > 1)
        {
            Debug.Log("hi");
            UIManager.instance.animalLeaveTimer.gameObject.SetActive(true);
            animalLeaveTimer -= Time.deltaTime;
            if (animalLeaveTimer < 0)
            {
                LeaveAnimal();
                animalLeaveTimer = 30f;
            }
        }
        else UIManager.instance.animalLeaveTimer.gameObject.SetActive(false);

        if (produce < 0.01f)
        {
            happiness -= 0.01f * Time.deltaTime;
        }

        if (happiness > 1f)
        {
            happiness = 1f;
        }
        if (happiness < 0)
        {
            happiness = 0;
        }

        if (elapsedTime > timer)
        {
            if (gold >= quota)
            {
                Debug.Log("You win!");
            }
            else
            {
                Debug.Log("You Lose...");
            }
        }

        minigameTimer -= Time.deltaTime;
        if (minigameTimer < 0)
        {
            UIManager.instance.minigameButton.interactable = true;
            UIManager.instance.minigameTimer.gameObject.SetActive(false);
        }
        
        
        if (gold - previousGold >= 1f)
        {
            previousGold = gold; // Store the whole number part of the current gold
            MoneyJustIncreased?.Invoke(); // Invoke the event if gold has increased by a whole number
        }
        
        if (elapsedTime >= timer)
        {
            GameOver?.Invoke();
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
        previousGold = gold;
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

    public bool Gamble(int bet)
    {
        if (bet <= (int)gold)
        {
            if (!hasGambledOnce)
            {
                hasGambledOnce = true;
                gold -= bet;
                return false; // Gamble lost
            }
            else
            {
                int chance = Random.Range(0, 100);
                if (chance < 50)
                {
                    gold -= bet;
                    return false; // Gamble lost
                }
                else
                {
                    gold += bet;
                    return true; // Gamble won
                }
            }
        }
        else
        {
            return false; // Not enough gold to gamble
        }
    }

    public void InstantiateMinigame()
    {
        Instantiate(minigamePrefabs[Random.Range(0, minigamePrefabs.Length)]);
    }

    public void ResetMinigameTimer()
    {
        minigameTimer = 20f;
        UIManager.instance.minigameButton.interactable = false;
        UIManager.instance.minigameTimer.gameObject.SetActive(true);
    }
}
