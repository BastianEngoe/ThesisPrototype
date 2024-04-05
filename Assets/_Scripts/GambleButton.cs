using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GambleButton : MonoBehaviour
{
    [SerializeField] private TMP_InputField textInput;
    [SerializeField] private GameObject errorMessage;
    private GameManager gameManager;
    
    private void Start()
    {
        if (!textInput)
        {
            var parent = transform.parent.transform;
            textInput = parent.Find("InputField").GetComponent<TMP_InputField>();
        }
        
        gameManager = GameManager.instance;
        
    }
    
    public void GambleButtonPress()
    {
        int.TryParse(textInput.text, out int bet);
        if (bet <= 0)
        {
            string message = "Invalid bet amount, must be greater than 0";
            Debug.Log(message);
            DisplayErrorMessage(message);
            return;
        }

        if (bet > gameManager.gold)
        {
            string message = "You don't have enough gold to gamble that amount";
            Debug.Log(message);
            DisplayErrorMessage(message);
            return;
        }

        bool gambleSuccessful = gameManager.Gamble(bet);

        if (!gambleSuccessful)
        {
            string message = "Gamble lost!";
            Debug.Log(message);
            DisplayErrorMessage(message);
        }
    }

    private void DisplayErrorMessage(string message)
    {
        GameObject errorObject = Instantiate(errorMessage, transform.parent);
        TMP_Text errorText = errorObject.GetComponentInChildren<TMP_Text>();
        if (errorText != null)
        {
            errorText.text = message;
        }
        Debug.Log(message);
    }
    
    
}
