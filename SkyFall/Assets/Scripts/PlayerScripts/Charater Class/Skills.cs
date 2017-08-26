public class Skills : ModifiedStats
{
    private bool _known;

    public Skills()
    {
        _known = false;
        XptoLvL = 25;
        LevelModifier = 1.1f;
    }

    public bool Known
    {
        get { return _known; }
        set {_known = value; }
    }
}

public enum SkillName
{
    Meele_Ofence,
    Meele_Defence,
    Range_Ofence,
    Range_Defence,
    Magic_Ofence,
    Magic_Defence
}
