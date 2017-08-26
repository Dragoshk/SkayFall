﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class myGUI : MonoBehaviour
{
    /*public GUISkin mySkin;
    public float buttonWidth = 40;
    public float buttonHeigth = 40;
    public float closeButtonWidith = 20;
    public float closeButtonHeigth = 20;

    private float _offset = 10f;
    private string _toolTip = "";

    /***************************************************/
    /*************Loot Window Variables****************/
    /***************************************************/
    //public static Chest _chest;
    /*public float lootWindowHeigth = 10;

    private bool _displayLootWindow = false;
    private Rect _lootWindowRect = new Rect(0, 0, 0, 0);
    private Vector2 _lootWindowSlider = Vector2.zero;
    private const int LOOT_WINDOWS_ID = 0;

    /***************************************************/
    /***********Inventory Window Variables*************/
    /***************************************************/
    /*private bool _displayInventoryWindow = true;
    private bool agoravai = false;
    private bool agoravail = false;
    private bool agoravair = false;
    private Rect _inventoryWindowRect = new Rect(10, 10, 300, 300);
    //private Vector2 _inventoryWindowSlider = Vector2.zero;
    private const int INVENTORY_WINDOWS_ID = 1;
    private int _inventoryRows = 6;
    private int _inventoryCols = 4;
    private float _dobleClickTimer = 0;
    private const float DOBLE_CLOCK_TIMER_THRESHHOLD = 3f;
    private Item _selectedItem;
    private Item itemSelecionado;
    //--- It will do magic Belive it or not-----\
    private bool BibbidiBobbidiBoo;
    private bool equipMainHand = false;

    /***************************************************/
    /***********Character Window Variables*************/
    /***************************************************/
    /*private bool _displayCharacterWindow = true;
    private Rect _characterWindowRect = new Rect(10, 10, 300, 340);
    //private Vector2 _inventoryWindowSlider = Vector2.zero;
    private const int CHARACTER_WINDOWS_ID = 2;
    private int _characterPanel = 0;
    private string[] _charaterPanelNames = new string[] { "Equipamente", "Attributes", "Skills" };

    /*
    void OnGUI()
    {
        GUI.skin = mySkin;

        //Loot
        if (_displayLootWindow == true)
            _lootWindowRect = GUI.Window(LOOT_WINDOWS_ID, new Rect(_offset, Screen.height - (_offset + lootWindowHeigth), Screen.width - (_offset * 80), lootWindowHeigth), LootWindow, "Loot Window", "BOX");

        //Inventory
        if (!_displayInventoryWindow)
            _inventoryWindowRect = GUI.Window(INVENTORY_WINDOWS_ID, _inventoryWindowRect, InventoryWindow, "Inventory");

        //Player panel
        if (!_displayCharacterWindow)
            _characterWindowRect = GUI.Window(CHARACTER_WINDOWS_ID, _characterWindowRect, CharacterWindow, "Character");

        //DisplayToolTip();
    }
    private void OnEnable()
    {
        Messenger.AddListener("DisplayLoot", DisplayLoot);
        Messenger.AddListener("Close", ClearWindow);
        Messenger.AddListener("ToggleInventory", ToggleInventory);
        Messenger.AddListener("ToggleCharacterWindow", ToggleCharacterWindow);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener("DisplayLoot", DisplayLoot);
        Messenger.RemoveListener("Close", ClearWindow);
        Messenger.RemoveListener("ToggleInventory", ToggleInventory);
        Messenger.RemoveListener("ToggleCharacterWindow", ToggleCharacterWindow);
    }
    */

    /*private void LootWindow(int id)
    {
        GUI.skin = mySkin;

        //botão fechar da janela de loot
        if (GUI.Button(new Rect(_lootWindowRect.width - 20, 0, closeButtonWidith, closeButtonHeigth), "x"))
        {
            ClearWindow();
        }

        if (_chest == null)
            return;

        //se os itens dentro do bau forem tds pegos ele apaga a janela do bau
        if (_chest.loot.Count == 0)
        {
            ClearWindow();
            return;
        }

        //barra de rolagem da janela de loot
        _lootWindowSlider = GUI.BeginScrollView(new Rect(5 * .5f, 15, _lootWindowRect.width - 10 * 2, 70), _lootWindowSlider, new Rect(0, 0, (_chest.loot.Count * buttonWidth) + _offset, buttonHeigth + _offset));

        //item dentro do bau
        for (int cnt = 0; cnt < _chest.loot.Count; cnt++)
        {
            /*
            if (GUI.Button(new Rect(5 + (buttonWidth * cnt), 5, buttonWidth, buttonHeigth), new GUIContent(_chest.loot[cnt].Name, _chest.loot[cnt].ToolTip())))
            {
                PlayerCharacter.Instance.Inventory.Add(_chest.loot[cnt]);
                _chest.loot.RemoveAt(cnt);

            }
            
        }

        GUI.DragWindow();
        GUI.EndScrollView();
        SetTooltip();
    } // define a janela de loot

    private void DisplayLoot()
    {
        _displayLootWindow = true;
    } // mostra a janela de loot

    private void ClearWindow()// apaga janela de loot
    {
        _chest.OnMouseUp();

        _displayLootWindow = false;
        _chest = null;
    }

    /*public void InventoryWindow(int id)// Janela do inventario
    {
        int cnt = 0;

        for (int y = 0; y < _inventoryRows; y++)
        {
            for (int x = 0; x < _inventoryCols; x++)
            {
                if (cnt < PlayerCharacter.Instance.Inventory.Count)
                {
                    if (GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeigth), buttonWidth, buttonHeigth),
            
                        new GUIContent(PlayerCharacter.Instance.Inventory[cnt].Name, PlayerCharacter.Instance.Inventory[cnt].ToolTip())))
                    {
                        // o correto eh != no if 
                        if (_dobleClickTimer != 0 && /* _selectedItem != null BibbidiBobbidiBoo == false)
                        {
                            
                            if (Time.time - _dobleClickTimer < DOBLE_CLOCK_TIMER_THRESHHOLD)
                            {
                                if (typeof(Weapon) == PlayerCharacter.Instance.Inventory[cnt].GetType())
                                {
                                   // Weapon wpe = (Weapon)(PlayerCharacter.Instance.Inventory[cnt]);

                                  

                                    switch (wpe.Slot)
                                    {
                                        case EquipmentSlot.MainHand:
                                            Debug.Log("Weapon main");
                                            if (PlayerCharacter.Instance.EquipedMainHand == null)
                                            {

                                                PlayerCharacter.Instance.EquipedMainHand = PlayerCharacter.Instance.Inventory[cnt];
                                                PlayerCharacter.Instance.Inventory.RemoveAt(cnt);
                                                Debug.Log(PlayerCharacter.Instance.EquipedMainHand + "||" + PlayerCharacter.Instance.Inventory[cnt]);
                                            }
                                            else
                                            {
                                                Item tmp = PlayerCharacter.Instance.EquipedMainHand;
                                                PlayerCharacter.Instance.EquipedMainHand = PlayerCharacter.Instance.Inventory[cnt];
                                                PlayerCharacter.Instance.Inventory[cnt] = tmp;
                                                agoravair = false;
                                            }
                                            break;
                                        case EquipmentSlot.OffHand:
                                            Debug.Log("Shield");

                                            if (PlayerCharacter.Instance.EquipedOffHand == null)
                                            {
                                                PlayerCharacter.Instance.EquipedOffHand = PlayerCharacter.Instance.Inventory[cnt];

                                                PlayerCharacter.Instance.Inventory.RemoveAt(cnt);
                                            }
                                            else
                                            {
                                                Item tmp = PlayerCharacter.Instance.EquipedOffHand;
                                                PlayerCharacter.Instance.EquipedOffHand = PlayerCharacter.Instance.Inventory[cnt];
                                                PlayerCharacter.Instance.Inventory[cnt] = tmp;

                                            }
                                            break;
                                    }

                                }


                                else if (typeof(Armor) == PlayerCharacter.Instance.Inventory[cnt].GetType())
                                {
                                   // Armor arm = (Armor)(PlayerCharacter.Instance.Inventory[cnt]);
                                    switch (arm.Slot)
                                    {
                                        case EquipmentSlot.Chest:


                                            if (PlayerCharacter.Instance.EquipedChest == null)
                                            {
                                                PlayerCharacter.Instance.EquipedChest = PlayerCharacter.Instance.Inventory[cnt];
                                                agoravai = true;
                                                PlayerCharacter.Instance.Inventory.RemoveAt(cnt);
                                            }
                                            else
                                            {
                                                Item tmp = PlayerCharacter.Instance.EquipedChest;
                                                PlayerCharacter.Instance.EquipedChest = PlayerCharacter.Instance.Inventory[cnt];
                                                PlayerCharacter.Instance.Inventory[cnt] = tmp;
                                            }

                                            break;
                                        default:
                                            Debug.Log("No defined equipament slot");
                                            break;
                                    }
                                }

                                _dobleClickTimer = 0;
                                _selectedItem = null;
                                BibbidiBobbidiBoo = true;
                            }
                            else
                            {
                                _dobleClickTimer = Time.time;
                            }

                        }

                        else
                        {
                            _dobleClickTimer = Time.time;
                            BibbidiBobbidiBoo = false;
                            //   PlayerCharacter.Instance.EquipedMainHand = PlayerCharacter.Instance.Inventory[cnt];
                            //           PlayerCharacter.Instance.EquipedMainHand.Add
                            //           PlayerCharacter.Instance.Inventory.Add(_chest.loot[cnt]);
                           
                        }
                    }
                }
                else
                {

                    GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeigth), buttonWidth, buttonHeigth), (x + y * _inventoryCols).ToString(), "box");
                }

                cnt++;
            }
        }
        SetTooltip();
        GUI.DragWindow();
    }

    public void ToggleInventory()
    {
        _displayInventoryWindow = !_displayInventoryWindow;
    }// troca a buleana da janela do inventario de verdadeiro para falso

    public void ToggleCharacterWindow()
    {
        _displayCharacterWindow = !_displayCharacterWindow;
    }// troca a buleana da janela do player panel de verdadeiro para falso

    public void CharacterWindow(int id)
    {
        _characterPanel = GUI.Toolbar(new Rect(5, 25, _characterWindowRect.width - 10, 50), _characterPanel, _charaterPanelNames);

        switch (_characterPanel)
        {
            case 0:
                DisplayEquipamente();
                break;

            case 1:
                DisplayAttributes();
                break;

            case 2:
                DisplaySkills();
                break;
        }

        GUI.DragWindow();
    }//janela do pplayer

    /*private void SetTooltip()
    {
        if (Event.current.type == EventType.repaint && GUI.tooltip != _toolTip)
        {
            if (_toolTip != "")
                _toolTip = "";

            if (GUI.tooltip != "")
                _toolTip = GUI.tooltip;
        }
    }// mostra detalhes do equipamento na janela do bau, no inventario ou no player panel depende da onde for chamado*/

    /*private void DisplayToolTip()
    {
        if (_toolTip != "")
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, 10, 200, 100), _toolTip);

        }


    }// mostra a janela de detalhes do item*/

    /*private void DisplayEquipamente()
    {
        GUI.BeginGroup(new Rect(5, 75, _characterWindowRect.width - (_offset * 2) + 10, _characterWindowRect.height), "", "box");

        if (PlayerCharacter.Instance.EquipedMainHand == null)
        {
            GUI.Label(new Rect(5, 5, 40, 40), "", "box");
        }

        if (PlayerCharacter.Instance.EquipedMainHand != null)
        {
            if (GUI.Button(new Rect(5, 5, 40, 40), new GUIContent(PlayerCharacter.Instance.EquipedMainHand.Name, PlayerCharacter.Instance.EquipedMainHand.ToolTip())))
            {
                PlayerCharacter.Instance.Inventory.Add(PlayerCharacter.Instance.EquipedMainHand);
                PlayerCharacter.Instance.EquipedMainHand = null;
            }
        }

        if (PlayerCharacter.Instance.EquipedOffHand == null)
        {
            GUI.Label(new Rect(5, 50, 40, 40), "", "box");
        }

        if (PlayerCharacter.Instance.EquipedOffHand != null)
        {
            if (GUI.Button(new Rect(5, 50, 40, 40), new GUIContent(PlayerCharacter.Instance.EquipedOffHand.Name, PlayerCharacter.Instance.EquipedOffHand.ToolTip())))
            {
                PlayerCharacter.Instance.Inventory.Add(PlayerCharacter.Instance.EquipedOffHand);
                PlayerCharacter.Instance.EquipedOffHand = null;
            }
        }

        SetTooltip();
        GUI.EndGroup();
    } // mostra a aba de equipamentos 

    private void DisplayAttributes() // mostra a aba de atributos
    {
        int linehigth = 17;

        GUI.BeginGroup(new Rect(5, 75, _characterWindowRect.width - (_offset * 2), _characterWindowRect.height), "", "Box");

        for (int cnt = 0; cnt < GameSettings2.pc.primaryAttribute.Length; cnt++)
        {
            GUI.Label(new Rect(0, cnt * linehigth, _characterWindowRect.width - (_offset * 2) - 45, 30), ((AtributeName)cnt).ToString());
            GUI.Label(new Rect(115, cnt * linehigth, 25, 30), GameSettings2.pc.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());
        }

        for (int cnt = 0; cnt < GameSettings2.pc.vital.Length; cnt++)
        {
            GUI.Label(new Rect(145, cnt * linehigth, _characterWindowRect.width - (_offset * 2) - 45, 30), ((VitalName)cnt).ToString());
            GUI.Label(new Rect(200, cnt * linehigth, 50, 30), GameSettings2.pc.GetVitals(cnt).CurValue + "/" + GameSettings2.pc.GetVitals(cnt).AdjustedBaseValue);
        }

        GUI.EndGroup();
    }

    private void DisplaySkills() // mostra a aba de skills 
    {
        int linehigth = 17;

        GUI.BeginGroup(new Rect(5, 75, _characterWindowRect.width - (_offset * 2), _characterWindowRect.height), "", "box");

        for (int cnt = 0; cnt < GameSettings2.pc.skills.Length; cnt++)
        {
            GUI.Label(new Rect(0, cnt * linehigth, _characterWindowRect.width - (_offset * 2) - 45, 30), ((SkillName)cnt).ToString());
            GUI.Label(new Rect(115, cnt * linehigth, 25, 30), GameSettings2.pc.GetSkills(cnt).AdjustedBaseValue.ToString());
        }

        GUI.EndGroup();
    }*/
}
