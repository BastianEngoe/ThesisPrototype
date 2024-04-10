using UnityEngine;
using UnityEngine.UI;

public class Minigame1 : MonoBehaviour
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
            animalAnim.Play("Eyes_Cry");
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
    }

    private void FinishMinigame()
    {
        GameManager.instance.happiness = 0;
        Destroy(gameObject);
    }
}
