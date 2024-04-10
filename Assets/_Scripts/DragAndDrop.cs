using UnityEngine;
using UnityEngine.EventSystems;

public interface IDragAndDrop
{
    void OnCurrentDrag();
}

public class DragAndDrop : MonoBehaviour, IDragHandler
{
    private IDragAndDrop onDrag;
    [SerializeField] private GameObject draggableObject;
    private void Start()
    {
        onDrag = draggableObject.GetComponent<IDragAndDrop>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag.OnCurrentDrag();
    }
}
