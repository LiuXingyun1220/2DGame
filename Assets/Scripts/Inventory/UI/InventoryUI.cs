using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("拖拽图片")]
        public Image dragItem;
        
        [SerializeField] private SlotUI[] playerSlots;

        [Header("背包设置")]
        [SerializeField] private int fireInitCnt;
        [SerializeField] private int waterInitCnt;
        [SerializeField] private int fireItemID = 1001; 
        [SerializeField] private int waterItemID = 1002; 


        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        }

        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        }

        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.Player:
                    for (int i = 0; i < playerSlots.Length; i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlots[i].UpdateSlot(item, list[i].itemAmount);
                        }
                        else
                        {
                            playerSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;
            }
        }

        private void Start()
        {
            for (int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }

            // 初始化物品数量
            InitializeInventoryItems();
        }

        /// <summary>
        /// 初始化背包中的物品数量
        /// </summary>
        private void InitializeInventoryItems()
        {
            // 设置火元素的初始数量
            if (fireInitCnt > 0)
            {
                InventoryManager.Instance.SetItemQuantity(fireItemID, fireInitCnt);
            }

            // 设置水元素的初始数量
            if (waterInitCnt > 0)
            {
                InventoryManager.Instance.SetItemQuantity(waterItemID, waterInitCnt);
            }
        }

        /// <summary>
        /// 更新Slot高亮显示
        /// </summary>
        /// <param name="index"></param>
        public void UpdateSlotHighlight(int index)
        {
            foreach (var slot in playerSlots)
            {
                if(slot.isSelected&&slot.slotIndex == index)
                {
                    slot.slotHightlight.gameObject.SetActive(true);
                }
                else
                {
                    slot.isSelected = false;
                    slot.slotHightlight.gameObject.SetActive(false);
                }
            }
        }
    }
}
