using UnityEngine;
using System.Collections.Generic;

public class EnemiesOnScene : MonoBehaviour
{
    public List<GameObject> enemies;
    // Use this for initialization
    void Start()
    {
        enemies = new List<GameObject>();
        AddEnemies();
    }

    void AddEnemies()
    {
        GameObject[] go = null;
        go = GameObject.FindGameObjectsWithTag("Enemy");
        enemies.Clear();
        foreach (GameObject enemy in go)
        {
            enemies.Add(enemy);
        }
    }
}
