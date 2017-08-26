using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,
    IPointerExitHandler, IDragHandler
{
    public Item item;
    Image itemImage;
    public int slotNumber;
    Inventory inventory;
    Text itemAmount;

    // Use this for initialization
    void Start()
    {
        itemAmount = gameObject.transform.GetChild(1).GetComponent<Text>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        itemImage = this.gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.Items[slotNumber].itemName != null)
        {
            itemAmount.enabled = false;
            //item = inventory.Items[slotNumber];
            itemImage.enabled = true;
            itemImage.sprite = inventory.Items[slotNumber].itemIcon;

            if (inventory.Items[slotNumber].itemType == Item.ItemType.Consumable)
            {
                itemAmount.enabled = true;
                itemAmount.text = "" + inventory.Items[slotNumber].itemValue;
            }
        }
        else
        {
            itemImage.enabled = false;
            itemAmount.enabled = false;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (data.button == PointerEventData.InputButton.Right)
        {
            if (inventory.Items[slotNumber].itemValue > 1 && !inventory.draggingItemBool
                && inventory.Items[slotNumber].itemType == Item.ItemType.Consumable)
            {
                int temp = inventory.Items[slotNumber].itemValue / 2;
                Item split = new Item(inventory.Items[slotNumber].itemName, inventory.Items[slotNumber].itemID,
                                      inventory.Items[slotNumber].itemDec, temp, inventory.Items[slotNumber].itemType);

                inventory.Items[slotNumber].itemValue -= temp;
                inventory.AddItemEmptySlot(split);
            }
        }

        if (data.button == PointerEventData.InputButton.Right)
        {
            if (inventory.Items[slotNumber].itemType == Item.ItemType.Consumable)
            {
                inventory.Items[slotNumber].itemValue--;
                if (inventory.Items[slotNumber].itemValue == 0)
                {
                    inventory.Items[slotNumber] = new Item();
                    itemAmount.enabled = false;
                    inventory.CloseToolTip();
                }
            }
        }

        if (inventory.Items[slotNumber].itemName == null)
        {
            if (inventory.draggingItemBool)
            {
                inventory.Items[slotNumber] = inventory.TheDraggedItem;
                inventory.CloseDraggedItem();
                //Debug.Log(inventory.Items[slotNumber].itemDec);
            }
        }
        else
        {
            try
            {
                if (inventory.draggingItemBool)
                {
                    if (inventory.Items[slotNumber].itemName == inventory.TheDraggedItem.itemName &&
                        inventory.TheDraggedItem.itemType == Item.ItemType.Consumable)
                    {
                        int temp = inventory.Items[slotNumber].itemValue;
                        inventory.Items[slotNumber] = new Item(inventory.TheDraggedItem.itemName, inventory.TheDraggedItem.itemID,
                                                               inventory.TheDraggedItem.itemDec, inventory.TheDraggedItem.itemValue + temp,
                                                               inventory.TheDraggedItem.itemType);
                        inventory.CloseDraggedItem();
                    }

                    if (inventory.Items[slotNumber].itemName != null)
                    {
                        inventory.Items[inventory.indexDraggedItem] = inventory.Items[slotNumber];
                        inventory.Items[slotNumber] = inventory.TheDraggedItem;
                        inventory.CloseDraggedItem();
                    }
                }
            }
            catch
            {

            }
        } 
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (inventory.Items[slotNumber].itemName != null && !inventory.draggingItemBool)
        {
            inventory.ShowToolTip(inventory.Slots[slotNumber].GetComponent<RectTransform>().localPosition,
                                  inventory.Items[slotNumber]);
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (inventory.Items[slotNumber].itemName != null)
        {
            inventory.CloseToolTip();
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (!inventory.draggingItemBool && data.button == PointerEventData.InputButton.Left)
        {
            if (inventory.Items[slotNumber].itemName != null)
            {
                inventory.ShowDraggedItem(inventory.Items[slotNumber], slotNumber);
                inventory.Items[slotNumber] = new Item();
                itemAmount.enabled = false;
            }
        }
    }
}
