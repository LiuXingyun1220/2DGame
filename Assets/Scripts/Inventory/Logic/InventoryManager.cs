using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("��Ʒ����")]
        public ItemDataList_SO itemDataList_SO;

        [Header("��������")]
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

        //��ָ��λ�������Ʒ
        private void AddItemAtIndex(int ID, int index, int amount) {
            if (index == -1&&CheckBagCapacit())//������û�������Ʒ���п�λ
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
            else//�������������Ʒ
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

        // ����� InventoryManager ����������·���

        /// <summary>
        /// ����ָ����Ʒ����
        /// </summary>
        /// <param name="itemID">��ƷID</param>
        /// <param name="amount">��������������>0��</param>
        /// <returns>�Ƿ�ɹ����٣�����Ʒ�����ڻ��������㷵��false��</returns>
        public bool RemoveItem(int itemID, int amount)
        {
            if (amount <= 0)
            {
                //Debug.LogError("���������������0");
                return false;
            }

            int index = GetItemIndexInBag(itemID);
            if (index == -1)
            {
                //Debug.LogWarning($"�����в�������ƷID: {itemID}");
                return false;
            }

            InventoryItem item = playerBag.itemList[index];
            if (item.itemAmount < amount)
            {
                //Debug.LogWarning($"��ƷID {itemID} �������㣬��ǰ������{item.itemAmount}�����Լ��٣�{amount}");
                return false;
            }

            // ִ����������
            int newAmount = item.itemAmount - amount;
            if (newAmount <= 0)
            {
                // ��������ʱ�����Ʒ��
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

            // ����UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
            return true;
        }
    }

}
