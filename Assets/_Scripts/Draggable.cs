using UnityEngine;

public class Draggable : MonoBehaviour, IDragAndDrop
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnCurrentDrag()
    {
        rectTransform.anchoredPosition = Input.mousePosition;
    }
}
