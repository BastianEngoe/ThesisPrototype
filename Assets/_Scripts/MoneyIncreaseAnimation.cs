using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyIncreaseAnimation : MonoBehaviour
{
    [SerializeField] private GameObject moneyIconToSpawn;

    private void OnEnable()
    {
        GameManager.MoneyJustIncreased += OnMoneyIncreased;
    }

    private void OnDisable()
    {
        GameManager.MoneyJustIncreased -= OnMoneyIncreased;
    }

    private void OnMoneyIncreased()
    {
        GameObject moneyIcon = Instantiate(moneyIconToSpawn, transform);
        StartCoroutine(MoveAndFade(moneyIcon));
    }

    IEnumerator MoveAndFade(GameObject moneyIcon)
    {
        var position = moneyIcon.transform.position;
        Vector3 startPosition = new Vector3(40, position.y, 0);
        Vector3 endPosition = startPosition + new Vector3(-5, 25, 1);
        Color startColor = moneyIcon.GetComponent<Image>().color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1);

        float duration = 1.0f; // duration of the animation in seconds
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;

            moneyIcon.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            moneyIcon.GetComponent<Image>().color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        Destroy(moneyIcon);
    }
}