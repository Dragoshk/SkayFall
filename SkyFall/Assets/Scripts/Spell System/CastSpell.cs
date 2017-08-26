using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpell : MonoBehaviour
{
    public Transform magicSpaw;
    public GameObject target;
    [HideInInspector]
    public Spell_Base spell;
    [SerializeField]
    public List<Spell_Base> spellList = new List<Spell_Base>();
    public float mana;
    
    public Ranged_Behavior rv;
    public AOE_Behavior aoe;

    void Start()
    {
        spell = (Spell_Base)Resources.Load("Spell/"+ gameObject.name,typeof(Spell_Base));
        List<Spell_Base> spellDatabase = GameObject.Find("SpellManager").GetComponent<SpellManager>().spellList;

        for (int i = 0; i < spellDatabase.Count;i++)
        {
            spellList.Add(spellDatabase[i]);
        }
    }

    public void CastMagic(Spell_Base spell)
    {
        if (spell.type == Spell_Base.SpellEffec.Ranged)
        {
            if (spell.spellPrefab.GetComponent<Ranged_Behavior>() == null)
            {
                spell.spellPrefab.AddComponent<Ranged_Behavior>();
            }

            rv = spell.spellPrefab.GetComponent<Ranged_Behavior>();
            rv.SpellBehavior(spell, magicSpaw,target);
        }

        if (spell.type == Spell_Base.SpellEffec.AEO)
        {
            if (spell.spellPrefab.GetComponent<AOE_Behavior>() == null)
            {
                spell.spellPrefab.AddComponent<AOE_Behavior>();
            }
            aoe = spell.spellPrefab.GetComponent<AOE_Behavior>();
            aoe.SpellBehavior(spell, magicSpaw);
        }
    }
}
