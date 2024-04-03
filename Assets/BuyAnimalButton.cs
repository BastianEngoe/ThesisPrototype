using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyAnimalButton : MonoBehaviour
{
    public void BuyAnimalButtonPress(string animalToBuy)
    {
        GameManager.instance.BuyAnimal(animalToBuy);
    }
}
