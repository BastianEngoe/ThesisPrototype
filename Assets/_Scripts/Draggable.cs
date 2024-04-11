using UnityEngine;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IDragAndDrop
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnCurrentDrag()
    {
        Vector2 localPosition = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, Input.mousePosition, null, out localPosition);

        rectTransform.position = rectTransform.TransformPoint(localPosition);
    }
}
