using UnityEngine;
using UnityEngine.UI;

public class InventoryDragHandler : MonoBehaviour
{
    public static InventoryDragHandler Instance;

    [SerializeField] private Image dragImage;

    public int DraggedFromIndex { get; private set; }
    public bool IsDragging { get; private set; }

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        Instance = this;
        dragImage.gameObject.SetActive(false);
        canvasGroup = dragImage.GetComponent<CanvasGroup>();
    }

    public void StartDrag(Sprite icon, int fromIndex)
    {
        dragImage.sprite = icon;
        dragImage.gameObject.SetActive(true);
        dragImage.raycastTarget = false; // não bloquear eventos nos slots

        DraggedFromIndex = fromIndex;
        IsDragging = true;
    }

    public void EndDrag()
    {
        dragImage.gameObject.SetActive(false);
        dragImage.raycastTarget = true;

        IsDragging = false;
    }

    private void Update()
    {
        if (IsDragging)
        {
            dragImage.transform.position = Input.mousePosition;
        }
    }
}
