using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Slots = new List<GameObject>();
    public List<Item> Items = new List<Item>();
    public GameObject slots;
    public GameObject ToolTip;
    public GameObject DraggedItemObject;
    public int x = -110;
    public int y = 100;
    public bool draggingItemBool = false;
    public Item TheDraggedItem;
    public int indexDraggedItem;
    ItemDatabase database;
    // Use this for initialization
    void Start()
    {
        int SlotAmount = 0;
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

        for (int i = 1; i < 6; i++)
        {
            for (int j = 1; j < 6; j++)
            {
                GameObject slot = Instantiate(slots) as GameObject;
                slot.GetComponent<SlotScript>().slotNumber = SlotAmount;
                Slots.Add(slot);
                Items.Add(new Item());
                slot.transform.SetParent(this.gameObject.transform);
                slot.name = "Slot" + i + "," + j;
                slot.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
                slot.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
                x = x + 55;
                if (j == 4)
                {
                    x = - 100;
                    y = y - 55;
                }
                SlotAmount++;
            }
        }
        
        AddItem(1);
        AddItem(2);
        AddItem(3);
        AddItem(4);
        AddItem(4);
        AddItem(4);
        AddItem(7);
        AddItem(9);
        AddItem(10);
        AddItem(13);
        AddItem(11);
        AddItem(12);
        AddItem(14);
        AddItem(15);
    }

    void Update()
    {
        if (draggingItemBool)
        {
            Vector3 pos = (Input.mousePosition - GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>().localPosition);
            DraggedItemObject.GetComponent<RectTransform>().localPosition = new Vector3(pos.x + 15, pos.y - 15, pos.z);
        }
    }

    void AddItem(int id)
    {
        for (int i = 0; i < database.items.Count;i++)
        {
            if (database.items[i].itemID == id)
            {
                Item item = database.items[i];

                if (database.items[i].itemType == Item.ItemType.Consumable)
                {
                    checkIfItemExist(id,item);
                    break;
                }
                else
                {
                    AddItemEmptySlot(item);
                }
            }
        }
    }

    public void checkIfItemExist(int id, Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemID == id)
            {
                Items[i].itemValue = Items[i].itemValue + 1;
                break;
            }
            else if(i == Items.Count -1)
            {
                AddItemEmptySlot(item);
            }
        }
    }

    public void AddItemEmptySlot(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].itemName == null)
            {
                Items[i] = item;
                break;
            }
        }
    }

    public void ShowToolTip(Vector3 toolPosition,Item item)
    {
        ToolTip.SetActive(true);
        ToolTip.GetComponent<RectTransform>().localPosition = new Vector3(toolPosition.x + 100, 
                                                              toolPosition.y, toolPosition.z);
        ToolTip.transform.GetChild(0).GetComponent<Text>().text = item.itemName;
        ToolTip.transform.GetChild(1).GetComponent<Text>().text = item.itemDec;
        ToolTip.transform.GetChild(2).GetComponent<Text>().text = item.itemType.ToString();
    }

    public void SwapItem()
    {

    }

    public void CloseToolTip()
    {
        ToolTip.SetActive(false);
    }

    public void ShowDraggedItem(Item item,int slotNumber)
    {
        indexDraggedItem = slotNumber;
        CloseToolTip();
        DraggedItemObject.SetActive(true);
        TheDraggedItem = item;
        draggingItemBool = true;
        DraggedItemObject.GetComponent<Image>().sprite = item.itemIcon;
    }

    public void CloseDraggedItem()
    {
        TheDraggedItem = new Item();
        draggingItemBool = false;
        DraggedItemObject.SetActive(false);
    }
}
