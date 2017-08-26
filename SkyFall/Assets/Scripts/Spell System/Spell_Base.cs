using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell_Base : ScriptableObject
{
    public string spellName = "Name me";
    public GameObject spellPrefab = null;
    public GameObject spellCollisionParticle = null;
    public Texture2D spellIcon = null;

    public int spellID = 0;
    public int spellCost = 0;
    public int spellMinDamage = 0;
    public int spellMaxDamage = 0;
    public float spellCoolDown = 0;
    public float spellCurrentCoolDown;

    public int projectilSpeed = 0;

    public SpellEffec type;
    public enum SpellEffec { Ranged,AEO}
    //public virtual void SpellBehavior(Spell_Base spell,Transform spaw);
}
