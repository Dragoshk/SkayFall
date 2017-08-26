using System;
using UnityEngine;

[Serializable]
public class Weapon : Item
{
    public WeaponType weaponType;
    public bool Ranged;

    public enum WeaponType { MainHand, OffHand, TwoHand }

    public Weapon(string name, int id, string desc, int value, ItemType type, WeaponType wtype,bool ranged)
    {
        itemName = name;
        itemID = id;
        itemDec = desc;
        itemValue = value;
        itemType = type;
        weaponType = wtype;
        Ranged = ranged;
        itemIcon = Resources.Load<Sprite>("Itens/Item Icons/" + name);
    }

    public Weapon()
    {

    }
}
