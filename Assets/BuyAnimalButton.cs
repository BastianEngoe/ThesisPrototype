using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAnimalButton : MonoBehaviour
{
    public void BuyAnimalButtonPress(string animalToBuy)
    {
        bool buySuccessful = GameManager.instance.BuyAnimal(animalToBuy);
    
        if (buySuccessful)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
}
