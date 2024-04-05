using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopButton : MonoBehaviour
{
    [SerializeField] GameObject shop;
    [SerializeField] private GameObject gamble;
    
    private void Start()
    {
        if (!shop)
        {
            shop = GameObject.Find("ShopCanvas");
        }
        shop.GetComponent<CanvasGroup>().alpha = 0;
        shop.GetComponent<CanvasGroup>().blocksRaycasts = false;
        shop.GetComponent<CanvasGroup>().interactable = false;

        if (!gamble)
        {
            gamble = GameObject.Find("GamblingCanvas");
        }
        gamble.GetComponent<CanvasGroup>().alpha = 0;
        gamble.GetComponent<CanvasGroup>().blocksRaycasts = false;
        gamble.GetComponent<CanvasGroup>().interactable = false;
    }
    
    public void OpenShop()
    {
        shop.GetComponent<CanvasGroup>().alpha = 1;
        shop.GetComponent<CanvasGroup>().blocksRaycasts = true;
        shop.GetComponent<CanvasGroup>().interactable = true;
    }

    public void OpenGamble()
    {
        gamble.GetComponent<CanvasGroup>().alpha = 1;
        gamble.GetComponent<CanvasGroup>().blocksRaycasts = true;
        gamble.GetComponent<CanvasGroup>().interactable = true;
    }
}
