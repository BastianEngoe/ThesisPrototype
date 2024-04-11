using System;
using UnityEngine;
using UnityEngine.UI;

public class Minigame2 : MonoBehaviour
{
    private GameObject animalToUse;
    private Animator animalAnim;
    private Slider animHappyGauge;
    private void OnEnable()
    {
        animalToUse = GameManager.instance.boughtAnimals[0];

        GameObject animalToSpawn = Instantiate(animalToUse, new Vector3(-250,-450,-50) , Quaternion.Euler(0, 130, 0), transform);
        animalToSpawn.transform.localPosition = new Vector3(-250,-450,-50);
        animalToSpawn.transform.localScale = new Vector3(683.25219f, 683.252197f, 683.252197f);
        animalAnim = animalToSpawn.GetComponent<Animator>();

        animHappyGauge = GetComponentInChildren<Slider>();
        
        animHappyGauge.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        
        animalAnim.Play("Eyes_Cry");
    }

    void ValueChangeCheck()
    {
        animalAnim.Play("Eat");
        if (animHappyGauge.value > 60)
        {
            animalAnim.Play("Eyes_Excited");
        }
        else
        {
            animalAnim.Play("Eyes_Cry");
        }
        
        if(animHappyGauge.value > 99)
        {
            Invoke("FinishMinigame", 1.25f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Hay"))
        {
            Debug.Log("Hay there");
            animHappyGauge.value += 20;
            Destroy(col.gameObject);
        }
    }
    
    public void CancelMinigame()
    {
        Destroy(gameObject);
    }

    private void FinishMinigame()
    {
        GameManager.instance.happiness = 0;
        Destroy(gameObject);
    }
}
