using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MyGame.Inventory;

public class SlotUI : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [Header("�����ȡ")]
    [SerializeField] private Image slotImage;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] public Image slotHightlight;
    [SerializeField] private Button button;

    [Header("��������")]
    public SlotType slotType;
    public bool isSelected;
    public int slotIndex;

    public ItemDetails itemDetails;
    public int itemAmount;

    private InventoryUI inventoryUI=>GetComponentInParent<InventoryUI>();

    private void Start()
    {
        isSelected = false;

        if (itemDetails.itemID==0) 
        { 
            UpdateEmptySlot();
        }
    }
    /// <summary>
    /// ����Slot��UI����Ϣ
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void UpdateSlot(ItemDetails item,int amount)
    {
        itemDetails=item;
        slotImage.sprite=item.itemIcon;
        slotImage.enabled = true;
        itemAmount=amount;
        amountText.text=amount.ToString();
        button.interactable = true;
    }

    /// <summary>
    /// ��Slot����Ϊ��
    /// </summary>
    public void UpdateEmptySlot()
    {
        if (isSelected)
        {
            isSelected = false;
        }

        slotImage.enabled = false;
        amountText.text=string.Empty;
        button.interactable = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemAmount == 0) return;
        isSelected = !isSelected;
        slotHightlight.gameObject.SetActive(isSelected);

        inventoryUI.UpdateSlotHighlight(slotIndex);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       if(itemAmount != 0)
        {
            inventoryUI.dragItem.enabled = true;
            inventoryUI.dragItem.sprite = slotImage.sprite;
            inventoryUI.dragItem.SetNativeSize();

            isSelected = true;
            inventoryUI.UpdateSlotHighlight(slotIndex);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        inventoryUI.dragItem.transform.position=Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventoryUI.dragItem.enabled = false;
        //Debug.Log(eventData.pointerCurrentRaycast.gameObject);

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
            {
                return;
            }

            var targetSlot= eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
            int targetIndex=targetSlot.slotIndex;

            if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
            {
                InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
            }

            inventoryUI.UpdateSlotHighlight(-1);
        }
        else
        {
            var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            EventHandler.CallInstantiateItemInScene(itemDetails.itemID, pos);
        }
    }
}
