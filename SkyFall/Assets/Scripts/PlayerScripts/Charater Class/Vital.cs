public class Vital : ModifiedStats
{
    private int _curValue;

    public Vital()
    {
        _curValue = 0;
        XptoLvL = 35;
        LevelModifier = 1.1f;
    }

    public int CurValue
    {
        get
        {
            if (_curValue > AdjustedBaseValue)
                _curValue = AdjustedBaseValue;

            return _curValue;
        }

        set {_curValue = value; }
    }
}

public enum VitalName
{
    Health,
    Energy,
    Mana
}
