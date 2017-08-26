using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public List<AI_State> AllEnemies = new List<AI_State>();
    public List<AI_State> EnemiesAvaliableToChase = new List<AI_State>();
    public List<AI_State> EnemiesOnPatrol = new List<AI_State>();

    public bool showBeraviors;
    public bool restAll;
    public bool universalAlert;
    public bool everyoneWhoCanChase;
    public bool patrolOnly;
    public Transform debugPOI;
	
	// Update is called once per frame
	void Update ()
    {
        if (restAll)
        {
            for (int i= 0;i < AllEnemies.Count;i++)
            {
                AllEnemies[i].ChangeToNormal();
            }

            restAll = false;
        }

        if (universalAlert)
        {
            for (int i = 0; i < AllEnemies.Count; i++)
            {
                AllEnemies[i].ChangeToAler(debugPOI.position);
            }

            universalAlert = false;
        }

        if (everyoneWhoCanChase)
        {
            for (int i = 0; i < EnemiesAvaliableToChase.Count; i++)
            {
                EnemiesAvaliableToChase[i].ChangeToAler(debugPOI.position);
            }

            everyoneWhoCanChase = false;
        }

        if (patrolOnly)
        {
            for (int i = 0; i < AllEnemies.Count; i++)
            {
                EnemiesOnPatrol[i].ChangeToAler(debugPOI.position);
            }

            patrolOnly = false;
        }

        if (showBeraviors)
        {
            for (int i = 0; i < AllEnemies.Count; i++)
            {
                //EnemiesOnPatrol[i].ChangeToAler(debugPOI.position);
            }

            showBeraviors = false;
        }
    }

    public void UpdateListOfEnemies()
    {
        if (AllEnemies.Count > 0)
        {
            for (int i = 0; i < AllEnemies.Count; i++)
            {
                if (AllEnemies[i].GetComponent<AI_State>().canChase)
                {
                    if (!EnemiesAvaliableToChase.Contains(AllEnemies[i]))
                    {
                        EnemiesAvaliableToChase.Add(AllEnemies[i]);
                    }
                }
            }
        }
    }
}
