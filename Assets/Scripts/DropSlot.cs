using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public bool isEmptySlot;
    public Text textField;
    public DragDrop DraggableIcon;
    public DropType dropType = DropType.None;

    public void Start()
    {
        if( isEmptySlot )
        {
            Destroy( transform.GetComponentInChildren<DragDrop>().gameObject );
            textField.enabled = true;
        }
        else
        {
            textField.enabled = false;
        }
    }

    public void ClearSlot()
    {
        isEmptySlot = true;
        textField.enabled = true;
    }

    public void FillSlot()
    {
        isEmptySlot = false;
        textField.enabled = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if ( eventData.pointerDrag != null)
        {
            DraggableIcon = eventData.pointerDrag.GetComponent<DragDrop>();
            DraggableIcon.transform.SetParent(transform);
            DraggableIcon.rectTransform.anchoredPosition = Vector2.zero;
            FillSlot();
        }
    }
}
