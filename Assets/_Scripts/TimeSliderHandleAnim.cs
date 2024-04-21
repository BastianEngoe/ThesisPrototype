using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSliderHandleAnim : MonoBehaviour
{
    private float originalScaleX;
    private float originalScaleY;

    void Start()
    {
        originalScaleX = transform.localScale.x;
        originalScaleY = transform.localScale.y;
    }

    void Update()
    {
        if (GameManager.instance.elapsedTime > 600)
        {
            float scaleY = Mathf.PingPong(Time.time, originalScaleY * 0.5f) + (originalScaleY); 
            transform.localScale = new Vector3(originalScaleX, scaleY, 1);
        }
    }
}