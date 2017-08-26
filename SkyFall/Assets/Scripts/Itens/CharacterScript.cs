using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //PlayerCononectMySql.InitateConection();

        for (int i= 0;i < 6;i++)
        {
            transform.GetChild(i).GetComponent<CharacterSlot>().index = i;
            //transform.GetChild(i).GetComponent<ActionBarSlot>().index = i;
        }

        //PlayerCononectMySql.GetItensForCharacter(PlayerCononectMySql._connection, 60);
        //int chest = PlayerCononectMySql._itensEquiped[0];
        //int hands = PlayerCononectMySql._itensEquiped[1];
        //int WeaponMain = PlayerCononectMySql._itensEquiped[2];
        //int Legs = PlayerCononectMySql._itensEquiped[3];
        //int Boots = PlayerCononectMySql._itensEquiped[4];
        //int WeaponOff = PlayerCononectMySql._itensEquiped[5];
        //Debug.Log(chest + hands + WeaponMain + Legs + Boots + WeaponOff);
    }
}
