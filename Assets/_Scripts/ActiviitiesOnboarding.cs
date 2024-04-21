using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiviitiesOnboarding : MonoBehaviour
{
    private bool onboarded = false;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        if (GameManager.instance.isGroupA)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
        else
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
    }

    private void Update()
    {
        if (GameManager.instance.elapsedTime >= 30 && GameManager.instance.isGroupA && !onboarded)
        {
            OnboardActivities();
            onboarded = true;
        }
    }

    private void OnboardActivities()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}