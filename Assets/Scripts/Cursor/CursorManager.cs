using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

namespace MyGame.Inventory {
    public class CursorManager : Singleton<CursorManager>
    {
        private Vector3 mouseWorldPos;

        private bool canClick;

        [Header("鼠标指针样式")]
        public Texture2D PointCursor;
        public Texture2D DragCursor;
        public Texture2D BrushCursor;

        private void Start()
        {
            SetPointCursor();
        }

        private void Update()
        {
            // 将鼠标屏幕位置转换为世界坐标并存储
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
                case "Teleport":
                    var teleport = clickObject.GetComponent<Teleport>();
                    teleport.TeleportToScene();
                    break;
            }
        }
        private Collider2D ObjectAtMousePosition()
        {
            return Physics2D.OverlapPoint(mouseWorldPos);
        }

        public void SetPointCursor()
        {
            Cursor.SetCursor(PointCursor, new Vector2(0, 0), CursorMode.Auto);
        }

        public void SetDragCursor()
        {
            Cursor.SetCursor(DragCursor, new Vector2(16, 16), CursorMode.Auto);
        }

        public void SetBrushCursor()
        {
            Cursor.SetCursor(BrushCursor, new Vector2(0, 32), CursorMode.Auto);
        }
    }
}

