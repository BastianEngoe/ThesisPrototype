using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupATutorialFocus : MonoBehaviour
{
    [SerializeField] private GameObject[] groupAObjects;
    [SerializeField] private GameObject clickToContinue;
    private bool canClick;
    private int currentObject = 0;
    private GameManager gamanager;
    private bool object3isDone;
    private bool object4isDone;
    
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
    }
    
    IEnumerator FirstOnboarding()
    {
        
        yield return new WaitForSeconds(2);

        groupAObjects[currentObject].SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(BlinkText());

    }
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick) // 0 is the left mouse button
        {
            StopCoroutine(BlinkText());
            canClick = false;
            clickToContinue.SetActive(false);

            Debug.Log("Current object is: "+currentObject);
            
            
            if (currentObject < groupAObjects.Length - 1)
            {
                groupAObjects[currentObject].SetActive(false);
                currentObject++;
            }
            
            if (currentObject < 3)
            {
                StopCoroutine(BlinkText());
                canClick = false;
                clickToContinue.SetActive(false);
                StartCoroutine(AnotherOnboarding());
            }
            
            Debug.Log("New current object is: "+currentObject);

            if (currentObject == 4)
            {

                StopCoroutine(BlinkText());
                canClick = false;
                clickToContinue.SetActive(false);
                Debug.Log("Click to continue should be disabled");
            }
        }
        
        if (gamanager.elapsedTime > 60 && currentObject == 4 && !object3isDone)
        {
            object3isDone = true;
            StartCoroutine(AnotherOnboarding());
        }
        
        if (gamanager.elapsedTime > 90 && currentObject == 5 && !object4isDone)
        {
            object4isDone = true;
            StartCoroutine(AnotherOnboarding());
        }
    }
    
    IEnumerator AnotherOnboarding()
    {
        groupAObjects[currentObject].SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(BlinkText());
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
        }
    }

}
