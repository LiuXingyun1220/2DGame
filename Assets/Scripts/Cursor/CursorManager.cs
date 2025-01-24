using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Inventory {
    public class CursorManager : MonoBehaviour
    {
        private Vector3 mouseWorldPos;

        private bool canClick;

        private void Update()
        {
            // �������Ļλ��ת��Ϊ�������겢�洢
            mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            //Debug.Log(mouseWorldPos.ToString());
            canClick = ObjectAtMousePosition();

            if (canClick && Input.GetMouseButtonDown(0))
            {
                ClickAction(ObjectAtMousePosition().gameObject);
            }
        }

        private void ClickAction(GameObject clickObject)
        {
            //Debug.Log(clickObject.name);
            switch (clickObject.tag)
            {
                case "Item":
                    Item item = clickObject.GetComponent<Item>();
                    if (item != null) { 
                        InventoryManager.Instance.AddItem(item,true);
                    }
                    break;
            }
        }
        private Collider2D ObjectAtMousePosition()
        {
            return Physics2D.OverlapPoint(mouseWorldPos);
        }
    }
}

