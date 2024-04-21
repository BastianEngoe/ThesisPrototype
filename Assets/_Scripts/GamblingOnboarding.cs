using UnityEngine;

public class GamblingOnboarding : MonoBehaviour
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
        if (GameManager.instance.elapsedTime >= 360 && GameManager.instance.isGroupA && !onboarded)
        {
            OnboardCasino();
            onboarded = true;
        }
    }

    private void OnboardCasino()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}