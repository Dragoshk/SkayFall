/// <summary>
/// BaseStats.cs
/// Pedro
/// 26/11/2015
/// 
/// Classe de base de tds os status do jogo
/// </summary>
public class BaseStats
{
    public const int STARTING_EXP_COST = 100;           //public accesable value for all base stats

    #region Private variables

    private int _baseValue;                             //base value of stats
    private int _buffValue;                             //valor de status do buff
    private int _xpToLvL;                               //quantidade de xp necessaria
    private float _lvlModifier;                         //modificador de xp necessaria pra passar de lvl
    private string _name;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseStats"/> class.
    /// </summary>
    public BaseStats()
    {
        _baseValue = 0;
        _buffValue = 0;
        _xpToLvL = (int)1.1f;
        _lvlModifier = STARTING_EXP_COST;
        _name = "";
    }

    #region Public Get and Set
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public int BaseValue
    {
        get {return _baseValue; }
        set {_baseValue = value; }
    }

    public int BuffValue
    {
        get {return _buffValue ; }
        set { _buffValue = value; }
    }

    public int XptoLvL
    {
        get {return _xpToLvL; }
        set {_xpToLvL = value; }
    }

    public float LevelModifier
    {
        get {return _lvlModifier; }
        set {_lvlModifier = value; }
    }
    #endregion

    private int CalculateXpToLvl()
    {
        return (int)(_xpToLvL * _lvlModifier);
    }

    public void LevelUp()
    {
        _xpToLvL = CalculateXpToLvl();
        _baseValue ++;
    }

    public int AdjustedBaseValue
    {
        get{ return _baseValue + _buffValue; }
    }
}
