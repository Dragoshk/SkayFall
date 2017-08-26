using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged_Behavior : MonoBehaviour
{
    GameObject spellobj;

    public void SpellBehavior(Spell_Base spell, Transform magicSpaw,GameObject target)
    {
        if (spell.spellPrefab == null)
        {
            Debug.LogWarning("Spell PreFab is null!!!! must asign a Spell PreFab");
            return;
        }
        else
        {
            spellobj = Instantiate(spell.spellPrefab, magicSpaw.position, magicSpaw.rotation) as GameObject;
            spellobj.AddComponent<Rigidbody>();
            spellobj.GetComponent<Rigidbody>().useGravity = false;
            spellobj.GetComponent<Rigidbody>().velocity = spellobj.transform.forward * spell.projectilSpeed;

            spellobj.name = spell.spellName;
            spellobj.transform.parent = GameObject.Find("SpellManager").transform;

            Destroy(spellobj, 2);
        }
    }
}
