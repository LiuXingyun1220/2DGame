using System.Collections;
using System.Collections.Generic;
using MyGame.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToPointCusor : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.SetPointCursor();
    }
}
