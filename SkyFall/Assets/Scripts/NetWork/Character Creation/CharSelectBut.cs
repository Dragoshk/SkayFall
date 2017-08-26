using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharSelectBut : MonoBehaviour
{
    [Header("Char")]
    public Text _Name;
    public Text Level;
    public int charID;
    public int Index;
    public int PointsLeft;
    public bool needpoints;
    public int _gender;
    public int _hair;
    public int _eyes;
    public int _hairColor;
    public int _eyesColor;
    public int _index;
    [Header("Itens")]
    public int chest;
    public int hands;
    public int WeaponMain;
    public int Legs;
    public int Boots;
    public int WeaponOff;
}
