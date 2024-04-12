using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame3 : MonoBehaviour
{
    private GameObject animalToUse;
    private Animator animalAnim;
    [SerializeField] private Slider animHappyGauge, powerGauge;
    [SerializeField] private Transform spawnPoint;
    
    private void OnEnable()
    {
        if (GameManager.instance.boughtAnimals.Count > 1)
        {
            animalToUse = GameManager.instance.boughtAnimals[Random.Range(0, GameManager.instance.boughtAnimals.Count)];
        }
        else animalToUse = GameManager.instance.boughtAnimals[0];

        GameObject animalToSpawn = Instantiate(animalToUse, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        animalAnim = animalToSpawn.GetComponent<Animator>();
        
        animalAnim.Play("Eyes_Trauma");
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            powerGauge.value += 120f * Time.deltaTime;
            
            if (animHappyGauge.value > 60)
            {
                animalAnim.Play("Eyes_Excited");
            }
            else animalAnim.Play("Eyes_Trauma");
        }
        else if (powerGauge.value > 1)
        {
            animHappyGauge.value += (powerGauge.value * 0.5f);
            powerGauge.value = 0;
        }

        if (powerGauge.value > 99)
        {
            powerGauge.value = 0;
        }
        
        if (animHappyGauge.value > 99)
        {
            Invoke("FinishMinigame", 1.25f);
            animalAnim.Play("Spin");
        }
    }
    
    public void CancelMinigame()
    {
        Destroy(gameObject);
        GameManager.instance.ResetMinigameTimer();
    }

    private void FinishMinigame()
    {
        GameManager.instance.happiness = 0;
        GameManager.instance.ResetMinigameTimer();
        Destroy(gameObject);
    }
}
