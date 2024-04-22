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
    
    private void Start()
    {
        gamanager = GameManager.instance;
        canClick = false;
        foreach (var obj in groupAObjects)
        {
            obj.SetActive(false);
        }
        
        if (GameManager.instance.isGroupA)
        {
            StartCoroutine(ShopOnboarding());
        }
    }
    
    IEnumerator ShopOnboarding()
    {
        
        yield return new WaitForSeconds(5);

        groupAObjects[currentObject].SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine(BlinkText());

    }
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick) // 0 is the left mouse button
        {
            canClick = false;
            clickToContinue.SetActive(false);
            StopCoroutine(BlinkText());
            if (currentObject < groupAObjects.Length - 1)
            {
                groupAObjects[currentObject].SetActive(false);
                currentObject++;
            }
        }
        
        if (gamanager.elapsedTime > 30)
        {
            StartCoroutine(ProductivityOnboarding());
        }
    }
    
    IEnumerator ProductivityOnboarding()
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
