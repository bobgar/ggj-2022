using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DropType
{ 
    None,
    Head,
    Arm,
    Feet,
    Body
}


public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform rectTransform;
    public DropType dropType = DropType.None;

    private Transform lastPosition;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Start()
    {
        canvas = PlanningSceneManager.Instance.gameCanvas;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.GetComponent<DropSlot>().ClearSlot();
        lastPosition = transform.parent;
        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter == null || 
            eventData.pointerEnter.GetComponent<DropSlot>() == null ||
            eventData.pointerEnter.GetComponent<DropSlot>().dropType != dropType)
        {
            transform.SetParent(lastPosition.transform);
            rectTransform.anchoredPosition = Vector2.zero;
            lastPosition.GetComponent<DropSlot>().FillSlot();
        }
        
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}