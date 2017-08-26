using System.Collections.Generic;

public class ModifiedStats : BaseStats
{
    private List<ModifyingAtribute> _mods;          //list of atributes q modificam os status
    private int _modValue;                          //the amount add to the basevalue from the modifiers

    public ModifiedStats()
    {
        _mods = new List<ModifyingAtribute>();
        _modValue = 0;
    }

    public void AddModifier(ModifyingAtribute mod)
    {
        _mods.Add(mod);
    }

    public void ClearModifiers()
    {
        _mods.Clear();
    }

    private void CalculateModValue()
    {
        _modValue = 0;

        if (_mods.Count > 0)
            foreach (ModifyingAtribute att in _mods)
                _modValue += (int)(att.atribute.AdjustedBaseValue * att.ratio);
    }

    public new int AdjustedBaseValue
    {
        get { return BaseValue + BuffValue + _modValue; }
    }

    public void UpDate()
    {
        CalculateModValue();
    }

    public string GetModifiedAttributesString()
    {
        string tmp = "";

        for (int cnt = 0; cnt < _mods.Count; cnt ++)
        {
            tmp += _mods[cnt].atribute.Name;
            tmp += ": ";
            tmp += _mods[cnt].atribute.AdjustedBaseValue;
            tmp += "-";
            tmp += _mods[cnt].ratio;

            if (cnt < _mods.Count - 1)
                tmp += "|";            
        }

        return "";
    }
}

public struct ModifyingAtribute
{
    public Atributes atribute;
    public float ratio;

    public ModifyingAtribute(Atributes att, float rat)
    {
        atribute = att;
        ratio = rat;
    }
}