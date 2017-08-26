using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CharacterSlot : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public int index;
    public Item item;
    public Inventory inventory;
    public PlayerModelCustomization Player;
    public DataWorker DW;
    public int slotMesh;
    public int gender;
    ItemDatabase database;
    [System.NonSerialized]
    public GameObject item0;
    [System.NonSerialized]
    public GameObject item1;
    [System.NonSerialized]
    public GameObject item2;
    [System.NonSerialized]
    public GameObject item3;
    [System.NonSerialized]
    public GameObject item4;
    [System.NonSerialized]
    public GameObject item5;

    public int chek;

    void Start()
    {
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        DW = GameObject.FindGameObjectWithTag("DataWorker").GetComponent<DataWorker>();
    }

    void Update()
    {
        slotMesh = Player.Generalindex;
        gender = Player.GeneralGender;


        if (item.itemType != Item.ItemType.None)
        {
            transform.GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetComponent<Image>().sprite = item.itemIcon;
        }
        else
        {
            transform.GetChild(0).GetComponent<Image>().enabled = false;
        }

        if (chek == 1)
        {
            AddItem(DW.chest, DW.hands, DW.WeaponMain, DW.Legs, DW.Boots, DW.WeaponOff);
            chek = 0;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (inventory.draggingItemBool)
        {
            #region Chest
            if (index == 0 && inventory.TheDraggedItem.itemType == Item.ItemType.Chest)
            {
                if (item.itemType != Item.ItemType.None)
                {
                    Item temp = item;
                    item = inventory.TheDraggedItem;
                    inventory.TheDraggedItem = temp;
                    inventory.ShowDraggedItem(temp, -1);
                    DW.chest = 0;
                }
                else
                {
                    DW.chest = inventory.TheDraggedItem.itemID;
                    item = inventory.TheDraggedItem;
                    inventory.CloseDraggedItem();

                    if (gender == 0)
                    {
                        item0 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + item.itemName),
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.position,
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.rotation)
                                            as GameObject;

                        item0.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform);
                    }
                    else
                    {
                        item0 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + item.itemName),
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.position,
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.rotation)
                                            as GameObject;

                        item0.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform);
                    }
                }
            }
            #endregion

            #region Hands
            if (index == 1 && inventory.TheDraggedItem.itemType == Item.ItemType.Hands)
            {
                if (item.itemType != Item.ItemType.None)
                {
                    Item temp = item;
                    item = inventory.TheDraggedItem;
                    inventory.TheDraggedItem = temp;
                    inventory.ShowDraggedItem(temp, -1);
                    DW.hands = 0;
                }
                else
                {
                    DW.hands = inventory.TheDraggedItem.itemID;
                    item = inventory.TheDraggedItem;
                    inventory.CloseDraggedItem();

                    //if (gender == 0)
                    //{
                    //    item1 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + item.itemName),
                    //                        Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.position,
                    //                        Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.rotation)
                    //                        as GameObject;
                    //item1.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform);
                    //}
                    //else
                    //{
                    //    item1 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + item.itemName),
                    //                        Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.position,
                    //                        Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.rotation)
                    //                        as GameObject;
                    //item1.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform);
                    //}
                }
            }
            #endregion

            #region Weapon
            if (index == 2 && inventory.TheDraggedItem.itemType == Item.ItemType.Weapon)
            {
                if (item.itemType != Item.ItemType.None)
                {
                    Item temp = item;
                    item = inventory.TheDraggedItem;
                    inventory.TheDraggedItem = temp;
                    inventory.ShowDraggedItem(temp, -1);
                    DW.WeaponMain = 0;
                }
                else
                {
                    if (typeof(Weapon) == inventory.TheDraggedItem.GetType())
                    {
                        Weapon wep = (Weapon)inventory.TheDraggedItem;
                        DW.WeaponMain = inventory.TheDraggedItem.itemID;
                        item = inventory.TheDraggedItem;
                        inventory.CloseDraggedItem();

                        if (wep.weaponType == Weapon.WeaponType.MainHand)
                        {
                            item2 = Instantiate(Resources.Load(GameSettings2.MELLE_WEAPON_MESH_PATH + item.itemName),
                               Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.position,
                               Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.rotation)
                               as GameObject;
                            item2.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform);
                        }

                        if (wep.weaponType == Weapon.WeaponType.TwoHand)
                        {
                            //Player.slots[slotMesh].GetComponent<Player_Controler>().WeaponT2h = true;

                            item2 = Instantiate(Resources.Load(GameSettings2.MELLE_WEAPON_MESH_PATH + item.itemName),
                               Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.position,
                               Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.rotation)
                               as GameObject;
                            item2.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform);
                        }
                    }
                }
            }
            #endregion

            #region Legs
            if (index == 4 && inventory.TheDraggedItem.itemType == Item.ItemType.Legs)
            {
                if (item.itemType != Item.ItemType.None)
                {
                    Item temp = item;
                    item = inventory.TheDraggedItem;
                    inventory.TheDraggedItem = temp;
                    inventory.ShowDraggedItem(temp, -1);
                    DW.Legs = 0;
                }
                else
                {
                    DW.Legs = inventory.TheDraggedItem.itemID;
                    item = inventory.TheDraggedItem;
                    inventory.CloseDraggedItem();

                    if (gender == 0)
                    {
                        item4 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + item.itemName),
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.position,
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.rotation)
                                            as GameObject;
                        item4.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform);
                    }
                    else
                    {
                        item4 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + item.itemName),
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.position,
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.rotation)
                                            as GameObject;
                        item4.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform);
                    }
                }
            }
            #endregion

            #region Boots
            if (index == 3 && inventory.TheDraggedItem.itemType == Item.ItemType.Boots)
            {
                if (item.itemType != Item.ItemType.None)
                {
                    Item temp = item;
                    item = inventory.TheDraggedItem;
                    inventory.TheDraggedItem = temp;
                    inventory.ShowDraggedItem(temp, -1);
                }
                else
                {
                    DW.Boots = inventory.TheDraggedItem.itemID;
                    item = inventory.TheDraggedItem;
                    inventory.CloseDraggedItem();

                    if (gender == 0)
                    {
                        item3 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + item.itemName),
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position,
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation)
                                            as GameObject;
                        item3.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform);
                    }
                    else
                    {
                        item3 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + item.itemName),
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position,
                                            Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation)
                                            as GameObject;
                        item3.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform);
                    }

                    Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().meshToHide3.SetActive(false);
                }
            }
            #endregion

            #region offhand
            if (index == 5 && inventory.TheDraggedItem.itemType == Item.ItemType.WeaponOff)
            {
                if (typeof(Weapon) == inventory.TheDraggedItem.GetType())
                {
                    Weapon wep = (Weapon)inventory.TheDraggedItem;
                    if (item.itemType != Item.ItemType.None)
                    {
                        Item temp = item;
                        item = inventory.TheDraggedItem;
                        inventory.TheDraggedItem = temp;
                        inventory.ShowDraggedItem(temp, -1);
                    }
                    else
                    {
                        DW.WeaponOff = inventory.TheDraggedItem.itemID;
                        item = inventory.TheDraggedItem;
                        inventory.CloseDraggedItem();

                        if (wep.weaponType == Weapon.WeaponType.MainHand)
                        {
                            item5 = Instantiate(Resources.Load(GameSettings2.MELLE_SHILD_MESH_PATH + item.itemName),
                              Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_R.transform.position,
                              Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_R.transform.rotation)
                              as GameObject;
                            item5.transform.SetParent(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_R.transform);
                        }
                    }
                }
            }
            #endregion
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (index == 0 && item.itemType != Item.ItemType.None)
        {
            DW.chest = 0;
            inventory.TheDraggedItem = item;
            inventory.ShowDraggedItem(item, -1);
            item = new Item();
            Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.GetChild(0).gameObject);
        }

        if (index == 1 && item.itemType != Item.ItemType.None)
        {
            DW.hands = 0;
            inventory.TheDraggedItem = item;
            inventory.ShowDraggedItem(item, -1);
            item = new Item();
            //Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.GetChild(0).gameObject);
        }

        if (index == 2 && item.itemType != Item.ItemType.None)
        {
            DW.WeaponMain = 0;
            inventory.TheDraggedItem = item;
            inventory.ShowDraggedItem(item, -1);
            item = new Item();

            if (Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.childCount != 0)
                Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.GetChild(0).gameObject);

            if (Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.childCount != 0)
                Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.GetChild(0).gameObject);

            if (Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.childCount != 0)
                Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.GetChild(0).gameObject);

            if (Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.childCount != 0)
                Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.GetChild(0).gameObject);
        }

        if (index == 3 && item.itemType != Item.ItemType.None)
        {
            DW.Boots = 0;
            inventory.TheDraggedItem = item;
            inventory.ShowDraggedItem(item, -1);
            item = new Item();
            Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.GetChild(0).gameObject);
        }

        if (index == 4 && item.itemType != Item.ItemType.None)
        {
            DW.Legs = 0;
            inventory.TheDraggedItem = item;
            inventory.ShowDraggedItem(item, -1);
            item = new Item();
            Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.GetChild(0).gameObject);
        }

        if (index == 5 && item.itemType != Item.ItemType.None)
        {
            DW.WeaponOff = 0;
            inventory.TheDraggedItem = item;
            inventory.ShowDraggedItem(item, -1);
            item = new Item();

            if (Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_BackL.transform.childCount != 0)
                Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_BackL.transform.GetChild(0).gameObject);

            if (Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_R.transform.childCount != 0)
                Destroy(Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_R.transform.GetChild(0).gameObject);
        }
    }

    void AddItem(int chest, int hands, int weapon, int legs, int boots, int weaponOff)
    {
        if (index == 0 && database.items[chest].itemType == Item.ItemType.Chest)
        {
            item = database.items[chest];
        }

        if (index == 1 && database.items[hands].itemType == Item.ItemType.Hands)
        {
            item = database.items[hands];
        }

        if (index == 2 && database.items[weapon].itemType == Item.ItemType.Weapon)
        {
            item = database.items[weapon];
        }

        if (index == 3 && database.items[boots].itemType == Item.ItemType.Boots)
        {
            item = database.items[boots];
        }

        if (index == 4 && database.items[legs].itemType == Item.ItemType.Legs)
        {
            item = database.items[legs];
        }

        if (index == 5 && database.items[weaponOff].itemType == Item.ItemType.WeaponOff)
        {
            item = database.items[weaponOff];
        }
    }
}
