using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitShopButton : MonoBehaviour
{
    [SerializeField] private GameObject shop;

    private void Start()
    {
        if (!shop)
        {
            shop = transform.parent.parent.gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitShop();
            if (GameManager.instance.currentActiveMinigame)
            {
               Destroy(GameManager.instance.currentActiveMinigame);
            }
            
        }
    }

    public void QuitShop()
    {
        shop.GetComponent<CanvasGroup>().alpha = 0;
        shop.GetComponent<CanvasGroup>().blocksRaycasts = false;
        shop.GetComponent<CanvasGroup>().interactable = false;
    }
}
