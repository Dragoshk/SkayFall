using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour
{
    public Player_Controler pc;
    public CastSpell cs;
    public List<Skill> skills = new List<Skill>();

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            skills[0].cooldown = cs.spellList[0].spellCoolDown;
            if (skills[0].currentCoolDown >= skills[0].cooldown)
            {
                skills[0].currentCoolDown = 0;
            }

            if (skills[0].currentCoolDown == 0 && cs.mana >= cs.spellList[0].spellCost)
            {
                cs.CastMagic(cs.spellList[0]);
                if (cs.spell != null)
                {
                    cs.CastMagic(cs.spell);
                }

                pc.anim.Play("Standing_2H_Magic_Attack_03");
                cs.mana -= cs.spellList[0].spellCost;
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            skills[1].cooldown = cs.spellList[1].spellCoolDown;
            if (skills[1].currentCoolDown >= skills[1].cooldown)
            {
                skills[1].currentCoolDown = 0;
            }

            if (skills[1].currentCoolDown == 0 && cs.mana >= cs.spellList[0].spellCost)
            {
                cs.CastMagic(cs.spellList[1]);
                if (cs.spell != null)
                {
                    cs.CastMagic(cs.spell);
                }

                pc.anim.Play("Standing_2H_Magic_Attack_03");
                cs.mana -= cs.spellList[0].spellCost;
            }
        }
    }

    void Update()
    {
        foreach (Skill s in skills)
        {
            if (s.currentCoolDown < s.cooldown)
            {
                s.currentCoolDown += Time.deltaTime;
                s.skillIcon.fillAmount = s.currentCoolDown / s.cooldown;
                s.Timer.text = ((int)s.currentCoolDown).ToString();
            }
        }
    }
}

[System.Serializable]
public class Skill
{
    public float cooldown;
    public Image skillIcon;
    public Text Timer;
    [HideInInspector]
    public float currentCoolDown;
}
