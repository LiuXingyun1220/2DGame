using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDataList_SO itemDataList_SO;

        [Header("背包数据")]
        public InventoryBag_SO playerBag;

        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
            //for (int i = 0; i < playerBag.itemList.Count; i++)
            //{
            //    Debug.Log(playerBag.itemList[i].itemID);
            //}

        }

        public ItemDetails GetItemDetails(int ID) {
            return itemDataList_SO.itemDetailsList.Find(i=>i.itemID==ID);
        }

        public void AddItem(Item item,bool toDestory)
        {
            var index=GetItemIndexInBag(item.itemID);
            AddItemAtIndex(item.itemID, index, 1);
          
            if (toDestory)
            {
                Destroy(item.gameObject);
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        private bool CheckBagCapacit()
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private int GetItemIndexInBag(int ID) {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == ID)
                {
                    return i;
                }
            }
            return -1;
        }

        //在指定位置添加物品
        private void AddItemAtIndex(int ID, int index, int amount) {
            if (index == -1&&CheckBagCapacit())//背包中没有这个物品且有空位
            {
                var item=new InventoryItem { itemID=ID, itemAmount=amount };
                for (int i = 0; i < playerBag.itemList.Count; i++)
                {
                    if (playerBag.itemList[i].itemID == 0)
                    {
                        playerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            else//背包中有这个物品
            {
                int currentAmount = playerBag.itemList[index].itemAmount + amount;
                var item = new InventoryItem { itemID = ID, itemAmount =currentAmount };
                playerBag.itemList[index] = item;
            }
        }

        public void SwapItem(int fromIndex, int targetIndex)
        {
            InventoryItem currentItem = playerBag.itemList[fromIndex];
            InventoryItem targetItem=playerBag.itemList[targetIndex];

            if (targetItem.itemID != 0)
            {
                playerBag.itemList[fromIndex]=targetItem;
                playerBag.itemList[targetIndex]=currentItem;
            }
            else
            {
                playerBag.itemList[targetIndex]=currentItem; ;
                playerBag.itemList[fromIndex]=new InventoryItem();
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        /// <summary>
        /// 减少指定物品数量
        /// </summary>
        /// <param name="itemID">物品ID</param>
        /// <param name="amount">减少数量（必须>0）</param>
        /// <returns>是否成功减少（若物品不存在或数量不足返回false）</returns>
        public bool RemoveItem(int itemID, int amount)
        {
            if (amount <= 0)
            {
                //Debug.LogError("减少数量必须大于0");
                return false;
            }

            int index = GetItemIndexInBag(itemID);
            if (index == -1)
            {
                //Debug.LogWarning($"背包中不存在物品ID: {itemID}");
                return false;
            }

            InventoryItem item = playerBag.itemList[index];
            if (item.itemAmount < amount)
            {
                //Debug.LogWarning($"物品ID {itemID} 数量不足，当前数量：{item.itemAmount}，尝试减少：{amount}");
                return false;
            }

            // 执行数量减少
            int newAmount = item.itemAmount - amount;
            if (newAmount <= 0)
            {
                // 数量归零时清空物品槽
                playerBag.itemList[index] = new InventoryItem();
            }
            else
            {
                playerBag.itemList[index] = new InventoryItem
                {
                    itemID = itemID,
                    itemAmount = newAmount
                };
            }

            // 更新UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
            return true;
        }

        /// <summary>
        /// 设置指定物品ID的数量
        /// </summary>
        /// <param name="itemID">物品ID</param>
        /// <param name="amount">要设置的数量（必须>=0）</param>
        /// <returns>是否成功设置（若物品不存在且背包已满，或amount为负数则返回false）</returns>
        public bool SetItemQuantity(int itemID, int amount)
        {
            // 检查数量是否有效
            if (amount < 0)
            {
                //Debug.LogError($"设置的物品数量不能为负数: {amount}");
                return false;
            }

            // 查找物品在背包中的索引
            int index = GetItemIndexInBag(itemID);

            // 如果设置数量为0，则移除物品
            if (amount == 0)
            {
                if (index != -1)
                {
                    // 清空该物品槽
                    playerBag.itemList[index] = new InventoryItem();
                    EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
                }
                return true;
            }

            // 物品已存在于背包中
            if (index != -1)
            {
                // 更新物品数量
                playerBag.itemList[index] = new InventoryItem
                {
                    itemID = itemID,
                    itemAmount = amount
                };
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
                return true;
            }
            else
            {
                // 物品不在背包中，需要添加
                if (CheckBagCapacit())
                {
                    // 找到空位并添加物品
                    var newItem = new InventoryItem { itemID = itemID, itemAmount = amount };
                    for (int i = 0; i < playerBag.itemList.Count; i++)
                    {
                        if (playerBag.itemList[i].itemID == 0)
                        {
                            playerBag.itemList[i] = newItem;
                            break;
                        }
                    }
                    EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
                    return true;
                }
                else
                {
                    // 背包已满，无法添加新物品
                    //Debug.LogWarning($"背包已满，无法添加新物品 ID: {itemID}");
                    return false;
                }
            }
        }
    }
}
