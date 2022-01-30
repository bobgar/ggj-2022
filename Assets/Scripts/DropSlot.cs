using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum BotType
{
    NONE,
    LEFT,
    RIGHT
}

public enum ArmLocation
{
    LEFT,
    RIGHT
}
public class DropSlot : MonoBehaviour, IDropHandler
{
    public bool isEmptySlot;
    public Text textField;
    public DragDrop DraggableIcon;
    public DropType dropType = DropType.None;

    public BotType bot;
    public ArmLocation armLocation;
    
    private Part currentPart;

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

        if (DraggableIcon != null && bot != BotType.NONE)
        {
            if (bot == BotType.LEFT)
            {
                PlanningSceneManager.Instance.LeftBot.RemovePiece(currentPart);
            }
            else
            {
                PlanningSceneManager.Instance.RightBot.RemovePiece(currentPart);
            }
        }
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
            AddPart(DraggableIcon);   
        }
    }

    public void AddPart(DragDrop DraggableIcon)
    {
        DraggableIcon.transform.SetParent(transform);
        DraggableIcon.rectTransform.anchoredPosition = Vector2.zero;
        FillSlot();

        if (bot == BotType.NONE)
        {
            return;
        }

        string partName = DraggableIcon.dropPart.ToString();

        if (dropType == DropType.Arm)
        {
            if (armLocation == ArmLocation.LEFT)
            {
                partName += "_LEFT";
            }
            else
            {
                partName += "_RIGHT";
            }
        }

        foreach (Part p in (Part[])Enum.GetValues(typeof(Part)))
        {
            if (p.ToString() == partName)
            {
                currentPart = p;
            }
        }

        if (bot == BotType.LEFT)
        {
            PlanningSceneManager.Instance.LeftBot.AddPiece(currentPart);
        }
        else
        {
            PlanningSceneManager.Instance.RightBot.AddPiece(currentPart);
        }
    }
}
