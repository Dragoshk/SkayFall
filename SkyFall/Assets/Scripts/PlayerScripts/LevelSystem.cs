using UnityEngine;
using System.Collections;

public class LevelSystem : MonoBehaviour
{
    public int level = 1;
    public int exp;
    public int maxexp = 100;
    public int curexp;

    void Start()
    {

    }

    void Update()
    {
        AdjustCurrentLevel();
    }

    private void AdjustCurrentLevel()
    {
        curexp = exp;

        if (curexp >= maxexp)
        {
            level++;
            maxexp *= 2;
            curexp = 0;
            exp = 0;
        }
    }
}
