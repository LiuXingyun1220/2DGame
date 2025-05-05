using System.Collections;
using System.Collections.Generic;
using MyGame.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToDragCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.SetDragCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.SetPointCursor();
    }
}
