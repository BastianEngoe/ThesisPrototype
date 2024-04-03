using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBalance : MonoBehaviour
{
    private TMP_Text moneyText;
    private GameManager gameManager;

    private void Start()
    {
        moneyText = GetComponent<TMP_Text>();
        gameManager = GameManager.instance;
    }

    void Update()
    {
        if (moneyText != null)
        {
            moneyText.text = "\u00a2" + gameManager.gold.ToString("F0");
        }
    }
}
