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
    }

}
