using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_Animations : MonoBehaviour
{
    public Animator anim;
    public bool WeaponT2h;
    public bool hasWeapon = false;
    public bool isBloking = false;

    void Start()
    {
        anim.GetComponent<Animator>();
    }

    public void Moving()
    {
        anim.SetBool("Moving", true);
        anim.SetInteger("CurrentAction", 0);
    }

    public void Running()
    {
        anim.SetBool("Rum", true);
        anim.SetBool("Fatigate", false);
    }

    public void TurningLeft()
    {
        anim.SetBool("TurningLeft", true);
        anim.SetInteger("CurrentAction", 0);
    }

    public void TurningRight()
    {
        anim.SetBool("TurningRight", true);
        anim.SetInteger("CurrentAction", 0);
    }

    public void NoTurningRight()
    {
        anim.SetBool("TurningRight", false);
    }

    public void NoTurningLeft()
    {
        anim.SetBool("TurningLeft", false);
    }

    public void NoMoving()
    {
        anim.SetBool("Moving", false);
    }

    public void NoRunning()
    {
        anim.SetBool("Rum", false);
        anim.SetBool("Fatigate", true);
    }

    public void Jumping()
    {
        anim.SetBool("Jumping", true);
        anim.SetInteger("CurrentAction", 0);
        Invoke("StopJumping", 0.1f);
    }

    public void Attacking_OneHand_R()
    {
        //anim.Play("OneHand_R Atack");
        //sword_and_shield_slash
        anim.Play("sword_and_shield_slash_2");
    }

    public void Attacking_TwoHand()
    {
        anim.Play("great_sword_slash");
    }

    public void InCombat()
    {
        if (!WeaponT2h)
            anim.SetInteger("inCombat", 1);
        else
            anim.SetInteger("inCombat", 2);
    }

    public void NotInCombat()
    {
        anim.SetInteger("inCombat", 0);
    }

    public void Falling()
    {
        anim.SetBool("Falling", true);
    }

    public void DranWeapon()
    {
        hasWeapon = true;
        if (WeaponT2h)
            anim.Play("undraw_a_great_sword_2");
        else
            anim.Play("draw_sword");
    }

    public void UnDrawWeapon()
    {
        hasWeapon = false;
        if (WeaponT2h)//draw_a_great_sword_1 undraw_a_great_sword_2
            anim.Play("draw_a_great_sword_1");
        else
            anim.Play("undraw_sword_1");
    }

    public void Bloking()
    {
        if (isBloking)
        {
            anim.SetInteger("isBloking", 1);
            if (!WeaponT2h)
                anim.SetBool("HasT2H", true);
            else
                anim.SetBool("HasT2H", true);
        }
        else
            anim.SetInteger("isBloking", 0);
    }
}
