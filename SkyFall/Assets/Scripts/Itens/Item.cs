using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public string itemName;
    public int itemID;
    public string itemDec;
    public Sprite itemIcon;
    public int itemValue;
    public ItemType itemType;

    public enum ItemType
    {
        None, Weapon,WeaponT2,WeaponOff, Ranged, Chest, Boots, Legs, Hands, Consumable
    }

    public Item(string name, int id, string desc, int value, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDec = desc;
        itemValue = value;
        itemType = type;
        itemIcon = Resources.Load<Sprite>("Itens/Item Icons/" + name);
    }

    public Item()
    {

    }
}
