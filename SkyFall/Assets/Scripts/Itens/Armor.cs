using System;
using UnityEngine;

[Serializable]
public class Armor : Item
{
    public int armorValue;

    public Armor(string name, int id, string desc, int value, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDec = desc;
        itemValue = value;
        itemType = type;
        itemIcon = Resources.Load<Sprite>("Itens/Item Icons/" + name);
    }

    public Armor()
    {

    }
}
