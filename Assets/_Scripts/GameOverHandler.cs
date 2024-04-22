using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    
    private void OnEnable()
    {
        GameManager.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameManager.GameOver -= OnGameOver;
    }

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void OnGameOver()
    {
        Time.timeScale = 0;
        canvasGroup.alpha = 1;
    }
}