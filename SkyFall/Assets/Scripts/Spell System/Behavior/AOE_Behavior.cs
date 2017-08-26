using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AOE_Behavior : MonoBehaviour
{
    GameObject spellobj;
    public SphereCollider sc;
    public float areaRadius;
    public float effectDuration;
    public float baseEffectDamage;
    public float damageTickDuration;

    private Stopwatch durationTimer = new Stopwatch();
    private bool isOccupied;

    public void SpellBehavior(Spell_Base spell, Transform magicSpaw)
    {
        if (spell.spellPrefab == null)
        {
            UnityEngine.Debug.LogWarning("Spell PreFab is null!!!! must asign a Spell PreFab");
            return;
        }
        else
        {
            spellobj = Instantiate(spell.spellPrefab, magicSpaw.position, magicSpaw.rotation) as GameObject;
            spellobj.transform.parent = GameObject.Find("SpellManager").transform;

            sc = spellobj.GetComponent<SphereCollider>();

            sc.radius = areaRadius;
            sc.isTrigger = true;

            AOE();

            Destroy(spellobj, 6);
        }

    }

    private void AOE()
    {
        durationTimer.Start();
        while (durationTimer.Elapsed.TotalSeconds <= effectDuration)
        {
            if (isOccupied)
            {
                //do damege
                UnityEngine.Debug.Log("tomo dano burro");
            }
            //yield return new WaitForSeconds(damageTickDuration);
        }

        durationTimer.Stop();
        durationTimer.Reset();

        //yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isOccupied)
        {
            //do damege
            UnityEngine.Debug.Log("tomo dano burro");
        }
        else
            isOccupied = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isOccupied = false;
    }
}
