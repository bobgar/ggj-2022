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

public enum DropPart
{
    //Chest
    CHEST,
    //Heads
    BASIC_HEAD,
    TANK_HEAD,
    //Feet
    WHEELS,
    TANK_TREADS,
    DRAGON_CLAW_FEET,
    //Arms
    SYTHE_ARM,
    WINDMILL_ARM,
    HAMMER_ARM,
}

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public RectTransform rectTransform;
    public DropType dropType = DropType.None;
    public DropPart dropPart;

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
            lastPosition.GetComponent<DropSlot>().AddPart(this);
        }
        
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;
    }
}