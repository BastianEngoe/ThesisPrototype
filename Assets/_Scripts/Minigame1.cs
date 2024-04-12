using UnityEngine;
using UnityEngine.UI;

public class Minigame1 : MonoBehaviour
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
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (animHappyGauge.value > 60)
            {
                animalAnim.Play("Eyes_Excited");
            }
            animHappyGauge.value += 10f * Time.deltaTime;
        }
        else
        {
            animalAnim.Play("Eyes_Trauma");
            animHappyGauge.value -= 5f * Time.deltaTime;
        }

        if (animHappyGauge.value < 0)
        {
            animHappyGauge.value = 0;
        }

        if (animHappyGauge.value > 99)
        {
            FinishMinigame();
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
