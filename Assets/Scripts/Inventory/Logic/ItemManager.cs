using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame.Inventory
{
    public class ItemManager : MonoBehaviour
    {
        public Item itemPrefab;
        private Transform itemParent;

        private void OnEnable()
        {
            EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
        }

        private void OnDisable()
        {
            EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;
        }

        private void Start()
        {
            itemParent = GameObject.FindWithTag("ItemParent").transform;
        }

        private void OnInstantiateItemInScene(int ID,Vector3 pos)
        {
            Debug.Log("…˙≥…¡À");
            var item = Instantiate(itemPrefab, pos, Quaternion.identity,itemParent);
            item.itemID = ID;
        }
    }
}

