using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject clickToContinue;
    
    
    
    
    private void Start()
    {
        GetComponent<Image>().color = new Color(0, 0, 0, 0.95f);
        GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(BlinkText());
        Time.timeScale = 0;
    }
    
    private IEnumerator BlinkText()
    {
        yield return new WaitForSecondsRealtime(2);
        while (true)
        {
            clickToContinue.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            clickToContinue.SetActive(false);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}
