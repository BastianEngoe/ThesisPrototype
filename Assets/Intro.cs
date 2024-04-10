using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject clicktoContinue;
    
    
    
    
    private void Start()
    {
        StartCoroutine(BlinkText());
    }
    
    private IEnumerator BlinkText()
    {
        yield return new WaitForSecondsRealtime(2);
        while (true)
        {
            clicktoContinue.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            clicktoContinue.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Destroy(gameObject);
        }
    }
}
