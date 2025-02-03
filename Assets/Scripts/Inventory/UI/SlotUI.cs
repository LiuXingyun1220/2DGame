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
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
