using UnityEngine;
using System.Collections;
using System;

public class BaseCharacter : MonoBehaviour
{
    private string _name;
    private int _level;
    private uint _freeExp;

    public Atributes[] primaryAttribute;
    public Vital[] vital;
    public Skills[] skills;

    public GameObject Weapon_Holder_R;
    public GameObject Weapon_Holder_L;
    public GameObject Chest_Holder;
    public GameObject Leggs_Holder;
    public GameObject Boots_Holder;
    public GameObject Hands_Holder;
    public GameObject HairAnchor;
    public GameObject EyesAnchor;

    public virtual void Awake()
    {
     
        _name = string.Empty;
        _level = 0;
        _freeExp = 0;

        primaryAttribute = new Atributes[Enum.GetValues(typeof(AtributeName)).Length];
        vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
        skills = new Skills[Enum.GetValues(typeof(SkillName)).Length];

        SetupPrimaryAttribute();
        SetupVitals();
        SetupSkills();
    }

    public string Name
    { get { return _name; } set { _name = value; } }

    public int Level
    { get { return _level; } set { _level = value; } }

    public uint FreeExp
    { get { return _freeExp; } set { _freeExp = value; } }

    public void AddExp(uint exp)
    {
        _freeExp += exp;
        CalculateLevel();
    }

    //take avg of all of the players lvl skills and assing that as player lvl
    public void CalculateLevel()
    {

    }

    private void SetupPrimaryAttribute()
    {
        for (int cnt = 0; cnt < primaryAttribute.Length; cnt++)
        {
            primaryAttribute[cnt] = new Atributes();
            primaryAttribute[cnt].Name = ((AtributeName)cnt).ToString();
        }
    }

    private void SetupVitals()
    {
        for (int cnt = 0; cnt < vital.Length; cnt++)
            vital[cnt] = new Vital();

        SetupVitalModifiers();
    }

    private void SetupSkills()
    {
        for (int cnt = 0; cnt < skills.Length; cnt++)
            skills[cnt] = new Skills();

        SetupSkillsModifiers();
    }

    public Atributes GetPrimaryAttribute(int index)
    {
        return primaryAttribute[index];
    }

    public Vital GetVitals(int index)
    {
        return vital[index];
    }

    public Skills GetSkills(int index)
    {
        return skills[index];
    }

    public void ClearModifiers()
    {
        for (int cnt = 0; cnt < vital.Length; cnt++)
            vital[cnt].ClearModifiers();

        for (int cnt = 0; cnt < skills.Length; cnt++)
            skills[cnt].ClearModifiers();

        SetupVitalModifiers();
        SetupSkillsModifiers();
    }

    private void SetupVitalModifiers()
    {
        //Heatl
        ModifyingAtribute health = new ModifyingAtribute();
        health.atribute = GetPrimaryAttribute((int)AtributeName.STAMINA);
        health.ratio = 1f;
        GetVitals((int)VitalName.Health).AddModifier(health);
        //Energy
        ModifyingAtribute energy = new ModifyingAtribute();
        energy.atribute = GetPrimaryAttribute((int)AtributeName.CONSTITUTION);
        energy.ratio = 1f;
        GetVitals((int)VitalName.Energy).AddModifier(energy);
        //Mana
        ModifyingAtribute mana = new ModifyingAtribute();
        mana.atribute = GetPrimaryAttribute((int)AtributeName.INTELECT);
        mana.ratio = 1f;
        GetVitals((int)VitalName.Mana).AddModifier(mana);
    }

    private void SetupSkillsModifiers()
    {
        //melle_ofenceve
        GetSkills((int)SkillName.Meele_Ofence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.STREGTH), .33f));
        GetSkills((int)SkillName.Meele_Ofence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.DEXTERITY), .33f));

        //melle_Defencive
        GetSkills((int)SkillName.Meele_Defence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.DEFENCERAT), .33f));
        GetSkills((int)SkillName.Meele_Defence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.ENDURENCE), .33f));

        //magic_ofence
        GetSkills((int)SkillName.Magic_Ofence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.SPELLPOWER), .33f));
        GetSkills((int)SkillName.Magic_Ofence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.WISDOW), .33f));

        //magic_ofence
        GetSkills((int)SkillName.Magic_Defence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.SPIRITPOWER), .33f));
        GetSkills((int)SkillName.Magic_Defence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.HAST), .33f));

        //ranged_ofenceve
        GetSkills((int)SkillName.Range_Ofence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.ATACKPOWER), .33f));
        GetSkills((int)SkillName.Range_Ofence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.AGILITY), .33f));

        //ranged_Defencive
        GetSkills((int)SkillName.Range_Defence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.HAST), .33f));
        GetSkills((int)SkillName.Range_Defence).AddModifier(new ModifyingAtribute(GetPrimaryAttribute((int)AtributeName.AGILITY), .33f));
    }

    public void StatsUpDate()
    {
        for (int cnt = 0; cnt < vital.Length; cnt++)
            vital[cnt].UpDate();

        for (int cnt = 0; cnt < skills.Length; cnt++)
            skills[cnt].UpDate();        
    }
}
