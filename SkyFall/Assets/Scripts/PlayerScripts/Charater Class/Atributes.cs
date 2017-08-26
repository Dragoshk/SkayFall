 public class Atributes : BaseStats
{
    new public const int STARTING_EXP_COST = 50;


    public Atributes()
    {
        XptoLvL = STARTING_EXP_COST;
        LevelModifier = 1.05f;
    }

}

public enum AtributeName
{
    STAMINA = 0,            //life
    CONSTITUTION = 1,       //Energy
    ATACKPOWER = 2,         //renged ofencive
    DEFENCERAT = 3,         //defencive meele
    WISDOW = 4,             //defencive magic
    ENDURENCE = 5,          //defencive meele
    AGILITY = 6,            //renged ofencive/defencive
    STREGTH = 7,            //ofencive meele
    INTELECT = 8,           //Mana
    SPELLPOWER = 9,         //ofencive magic
    SPIRITPOWER = 10,       //defencive magic
    HAST = 11,              //ofencive magic
    DEXTERITY = 12          //ofencive meele
}