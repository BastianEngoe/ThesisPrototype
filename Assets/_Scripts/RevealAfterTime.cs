using UnityEngine;

public class RevealAfterTime : MonoBehaviour
{
    [SerializeField] private float timeToReveal = 360;
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
        if (GameManager.instance.elapsedTime >= timeToReveal && GameManager.instance.isGroupA && !onboarded)
        {
            OnboardReveal();
            onboarded = true;
        }
    }

    private void OnboardReveal()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}