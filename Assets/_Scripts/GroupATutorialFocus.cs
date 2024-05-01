using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GroupATutorialFocus : MonoBehaviour
{
    [SerializeField] private GameObject[] groupAObjects;
    [SerializeField] private GameObject clickToContinue;
    private bool canClick;
    private int currentObject = 0;
    private GameManager gamanager;
    private bool object3isDone;
    private bool object4isDone;
    private bool object5isDone;
    [FormerlySerializedAs("shopCanvas")] [SerializeField]private CanvasGroup shopBtnCanvas;
    [SerializeField] private CanvasGroup shopPanel;
    [SerializeField] private GameObject Minigamer;

    
    private void Start()
    {
        clickToContinue.SetActive(false);
        gamanager = GameManager.instance;
        canClick = false;
        foreach (var obj in groupAObjects)
        {
            obj.SetActive(false);
        }
        
        if (GameManager.instance.isGroupA)
        {
            StartCoroutine(FirstOnboarding());
        }
        else
        {
            shopBtnCanvas.alpha = 1;
            shopBtnCanvas.interactable = true;
            shopBtnCanvas.blocksRaycasts = true;
        }
    }
    
    IEnumerator FirstOnboarding()
    {
        
        yield return new WaitForSeconds(2);

        groupAObjects[currentObject].SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowClickToContinue());

    }
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick) // 0 is the left mouse button
        {
            canClick = false;
            clickToContinue.SetActive(false);

            Debug.Log("Current object is: "+currentObject);
            
            
            if (currentObject < groupAObjects.Length)
            {
                groupAObjects[currentObject].SetActive(false);
            }

            if (currentObject < groupAObjects.Length - 1)
            {
                currentObject++;
            }

            if (currentObject < 4)
            {
                
                canClick = false;
                clickToContinue.SetActive(false);
                StartCoroutine(AnotherOnboarding());
            }
            
            if (currentObject == 3)
            {
                shopBtnCanvas.alpha = 1;
                shopBtnCanvas.interactable = true;
                shopBtnCanvas.blocksRaycasts = true;
            }

            if (currentObject == 4 && !object3isDone)
            {
                object3isDone = true;
            }
            
            Debug.Log("New current object is: "+currentObject);

            if (currentObject == 4)
            {
                
                canClick = false;
                clickToContinue.SetActive(false);
                Debug.Log("Click to continue should be disabled");
            }


            if (currentObject == 5)
            {
                
                canClick = false;
                clickToContinue.SetActive(false);
            }
            
            
            
            // if (currentObject > 4)
            // {
            //     StopAllCoroutines();
            // }

            // if (currentObject == 6)
            // {
            //     StartCoroutine(BlinkText());
            // }
        }
        
        if (gamanager.elapsedTime > 60 && currentObject == 4 && !object4isDone)
        {
            object4isDone = true;
            StartCoroutine(AnotherOnboarding());
            shopPanel.alpha = 0;
            shopPanel.interactable = false;
            shopPanel.blocksRaycasts = false;
        }
        
        
        
        // if (currentObject > 4)
        // {
        //     StopAllCoroutines();
        // }
        
        if (gamanager.elapsedTime > 90 && currentObject == 5 && !object5isDone)
        {
            object5isDone = true;
            StartCoroutine(AnotherOnboarding());
            StartCoroutine(ShowClickToContinue());
            Debug.Log("The current object is: "+currentObject);
            shopPanel.alpha = 0;
            shopPanel.interactable = false;
            shopPanel.blocksRaycasts = false;
            if (gamanager.currentActiveMinigame){
                
              Destroy(gamanager.currentActiveMinigame);
            
            }
            canClick = true;
        }
    }
    
    IEnumerator AnotherOnboarding()
    {
        Debug.Log("started another onboarding");
        canClick = false;
        clickToContinue.SetActive(false);
        groupAObjects[currentObject].SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowClickToContinue());
    }

    private IEnumerator BlinkText()
    {
        canClick = true;
        while (true)
        {
            clickToContinue.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            clickToContinue.SetActive(false);
            yield return new WaitForSecondsRealtime(0.5f);
            if (currentObject == 5 && object5isDone)
            {
                clickToContinue.SetActive(false);
                yield break;
            }
            if (currentObject == 4 && object4isDone)
            {
                clickToContinue.SetActive(false);
                yield break;
            }
        }
        
    }

    private IEnumerator ShowClickToContinue()
    {

        yield return new WaitForSecondsRealtime(3);
        canClick = true;
        clickToContinue.SetActive(true);

    }

}
