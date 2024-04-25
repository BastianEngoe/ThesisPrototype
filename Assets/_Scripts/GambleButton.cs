using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GambleButton : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject errorMessage;
    [SerializeField] private Slider goldSlider;
    private GameManager gameManager;
    
    private void Start()
    {

        gameManager = GameManager.instance;
        
    }
    
   public void GambleButtonPress()
    {
        int bet = (int)goldSlider.value;
        if (bet <= 0)
        {
            string message = "Invalid bet amount, must be greater than 0";
            Debug.Log(message);
            DisplayErrorMessage(message, errorMessage.GetComponent<TMP_Text>().color);
            return;
        }

        if (bet > gameManager.gold)
        {
            string message = "You don't have enough gold to gamble that amount";
            Debug.Log(message);
            DisplayErrorMessage(message, errorMessage.GetComponent<TMP_Text>().color);
            return;
        }

        bool gambleSuccessful = gameManager.Gamble(bet);

        if (!gambleSuccessful)
        {
            string message = "Gamble lost!";
            Debug.Log(message);
            DisplayErrorMessage(message, errorMessage.GetComponent<TMP_Text>().color);
        }
        else
        {
            string message = "Gamble won!";
            Debug.Log(message);
            DisplayErrorMessage(message, Color.green);
        }
    }

    private void DisplayErrorMessage(string message, Color color)
    {
        GameObject errorObject = Instantiate(errorMessage, transform.parent);
        TMP_Text errorText = errorObject.GetComponentInChildren<TMP_Text>();
        Animator errorAnimator = errorObject.GetComponent<Animator>();
        
        if (errorAnimator != null)
        {
            errorAnimator.speed = 0.4f;
        }
        if (errorText != null)
        {
            errorText.text = message;
            errorText.color = color;
        }
        Debug.Log(message);
    }

    private void Update()
    {
        goldSlider.maxValue = (int)gameManager.gold;
        buttonText.text = "GAMBLE " + (int)goldSlider.value + " GOLD";
    }
}
