using UnityEngine;
using System.Collections;

public class Mob : BaseCharacter
{
    void Start()
    {
        GetPrimaryAttribute((int)AtributeName.CONSTITUTION).BaseValue = 100;
        GetVitals((int)VitalName.Health).UpDate();
    }

    void UpDate()
    {
        //Messenger<int, int>.Broadcast("Mob health changed", 80, 100);
    }
}

