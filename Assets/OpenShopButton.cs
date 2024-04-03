using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopButton : MonoBehaviour
{
    [SerializeField] GameObject shop;
    
    private void Start()
    {
        if (!shop)
        {
            shop = GameObject.Find("ShopCanvas");
        }
        shop.GetComponent<CanvasGroup>().alpha = 0;
        shop.GetComponent<CanvasGroup>().blocksRaycasts = false;
        shop.GetComponent<CanvasGroup>().interactable = false;
    }
    
    public void OpenShop()
    {
        shop.GetComponent<CanvasGroup>().alpha = 1;
        shop.GetComponent<CanvasGroup>().blocksRaycasts = true;
        shop.GetComponent<CanvasGroup>().interactable = true;
    }
}
