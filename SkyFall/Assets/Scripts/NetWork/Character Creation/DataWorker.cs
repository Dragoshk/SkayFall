using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWorker : MonoBehaviour
{
    //Character
    [Header("Character")]
    public string _name;
    public int _gender;
    public int _id;
    public int _hair;
    [System.NonSerialized]
    public int _eyes;
    public int _hairColor;
    public int _eyesColor;
    public int _index;

    //Itens
    [Header("Itens")]
    public int chest;
    public int hands;
    public int WeaponMain;
    public int Legs;
    public int Boots;
    public int WeaponOff;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
