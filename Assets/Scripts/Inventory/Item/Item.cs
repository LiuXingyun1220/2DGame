using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

namespace MyGame.Inventory {
    public class Item : MonoBehaviour
    {
        public int itemID;

        private SpriteRenderer spriteRenderer;
        private ItemDetails itemDetails;

        private BoxCollider2D coll;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            coll = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            if (itemID != 0) 
            {
                Init(itemID);
            }
        }

        public void Init(int ID)
        {
            itemID= ID;

            //Inventory获取当前数据
            itemDetails=InventoryManager.Instance.GetItemDetails(itemID);

            if(itemDetails != null)
            {
                spriteRenderer.sprite = itemDetails.itemOnWorldSprite!=null ? itemDetails.itemOnWorldSprite :itemDetails.itemIcon;

                //修改碰撞体尺寸
                Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
                coll.size = newSize;
                coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);
            }

            //根据类型初始化组件
            switch (itemDetails.itemType)
            {
                case ItemType.Fire:
                    gameObject.AddComponent<FireBehaviour>();
                    break;
                case ItemType.Water:
                    gameObject.AddComponent<WaterBehaviour>();
                    break;

            }

        }

        private void AddFireComponent()
        {
            
            
            Debug.Log("火堆组件已添加");
        }
    }
}


