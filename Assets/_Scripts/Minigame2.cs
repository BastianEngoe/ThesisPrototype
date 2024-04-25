using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Minigame2 : MonoBehaviour
{
    private GameObject animalToUse;
    private Animator animalAnim;
    private Slider animHappyGauge;
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

        animHappyGauge = GetComponentInChildren<Slider>();
        
        animHappyGauge.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        
        animalAnim.Play("Eyes_Trauma");
    }

    private void Start()
    {
        GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        GetComponent<Canvas>().worldCamera = Camera.main;
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
            animalAnim.Play("Eyes_Trauma");
        }
        
        if(animHappyGauge.value > 99)
        {
            Invoke("FinishMinigame", 1.25f);
            animalAnim.Play("Spin");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Hay"))
        {
            animHappyGauge.value += 20;
            Destroy(col.gameObject);
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
