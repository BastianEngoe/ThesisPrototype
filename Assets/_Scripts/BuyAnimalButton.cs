using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuyAnimalButton : MonoBehaviour
{
    [SerializeField] private string animalToBuy;
    private Button button;
    private Image image;
    [SerializeField] private int maxAnimals = 5;

    [SerializeField] GameObject errorMessage;

    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        StartCoroutine(BounceButton());
    }

    
    public void BuyAnimalButtonPress()
    {
        bool buySuccessful = GameManager.instance.BuyAnimal(animalToBuy);

        if (!buySuccessful)
        {
            Debug.Log("Not enough money!");
            Instantiate(errorMessage, transform.parent);
        }
        else
        {
            maxAnimals--;
        }

        if (maxAnimals == 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        bool canBuy = GameManager.instance.gold >= GetAnimalCost(animalToBuy);

        if (!canBuy)
        {
            image.color = new Color(99/255f, 67/255f, 67/255f); // #634343
        }
        else
        {
            image.color = Color.white;
        }
    }

    private float GetAnimalCost(string animal)
    {
        switch (animal)
        {
            case "Chicken":
                return GameManager.instance.costOfChicken;
            case "Sheep":
                return GameManager.instance.costOfSheep;
            case "Pig":
                return GameManager.instance.costOfPig;
            case "Cow":
                return GameManager.instance.costOfCow;
            default:
                return 0;
        }
    }
    
    IEnumerator BounceButton()
    {
        bool canBuy = GameManager.instance.gold >= GetAnimalCost(animalToBuy);
        while (canBuy){
            Vector3 originalScale = transform.localScale;
            Vector3 targetScale = originalScale * 1.05f;
            float time = 0;

            // Scale up
            while (time <= 0.35f)
            {
                transform.localScale = Vector3.Lerp(originalScale, targetScale, time / 0.25f);
                time += Time.deltaTime;
                yield return null;
            }

            time = 0;

            // Scale down
            while (time <= 0.35f)
            {
                transform.localScale = Vector3.Lerp(targetScale, originalScale, time / 0.25f);
                time += Time.deltaTime;
                yield return null;
            }

            // Ensure the scale is reset to the original scale
            transform.localScale = originalScale;
        }

        transform.localScale = Vector3.one;
    }
    
}
