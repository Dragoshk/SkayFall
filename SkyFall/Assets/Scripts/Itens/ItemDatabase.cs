using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    // Use this for initialization
    void Awake()
    {
        //null item
        items.Add(new Item("null", 0, "null", 0, Item.ItemType.None));
        //items.Add(new Armor("A_Armor05", 1, "chest", 10, Armor.ItemType.Chest));
        items.Add(new Item("A_Armor05", 1, "chest", 10, Item.ItemType.Chest));
        items.Add(new Item("A_Shoes04", 2, "chest", 10, Item.ItemType.Boots));
        items.Add(new Weapon("Espada", 3, "chest", 10, Weapon.ItemType.Weapon,Weapon.WeaponType.MainHand,false));
        items.Add(new Item("Antidote", 4, "chest", 10, Item.ItemType.Consumable));
        items.Add(new Item("Banana", 5, "chest", 10, Item.ItemType.Consumable));
        items.Add(new Item("I_BatWing", 6, "chest", 10, Item.ItemType.WeaponOff));
        items.Add(new Item("A_Shoes06", 7, "chest", 10, Item.ItemType.Boots));
        items.Add(new Item("Ac_Gloves06", 8, "chest", 10, Item.ItemType.Hands));
        items.Add(new Item("Ac_Gloves07", 9, "chest", 10, Item.ItemType.Hands));
        items.Add(new Item("E_Gold01", 10, "chest", 10, Item.ItemType.WeaponOff));
        items.Add(new Item("Ac_Necklace04", 11, "chest", 10, Item.ItemType.Legs));
        items.Add(new Item("Ac_Necklace03", 12, "chest", 10, Item.ItemType.Legs));
        items.Add(new Item("A_Clothing01", 13, "chest", 10, Item.ItemType.Chest));
        items.Add(new Weapon("EspadaDuasMao", 14, "chest", 10, Weapon.ItemType.Weapon,Weapon.WeaponType.TwoHand,false));
        items.Add(new Weapon("W_Sword007", 15, "chest", 10, Weapon.ItemType.Weapon,Weapon.WeaponType.TwoHand,false));
    }
}
