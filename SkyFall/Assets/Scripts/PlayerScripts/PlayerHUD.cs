using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerHUD : MonoBehaviour
{
    public float maxHeatlh;
    public float curHeatlh;
    public float maxMana;
    public float curMana;
    public float maxStamina;
    [System.NonSerialized]
    public float curStamina;
    [System.NonSerialized]
    public bool dead = false;
    [System.NonSerialized]
    public bool Fatigued = false;
    [SerializeField]
    private Stats _stamina;
    [SerializeField]
    private Stats _health;
    [SerializeField]
    private Stats _mana;
    [SerializeField]

    private void Awake()
    {
        _health.Initialize();
        _mana.Initialize();
        _stamina.Initialize();
    }
    void Update()
    {
        _health.MaxValue = maxHeatlh;
        _mana.MaxValue = maxMana;
        _health.CurrentVal = curHeatlh;
        _mana.CurrentVal = curMana;
        _stamina.MaxValue = maxStamina;
        _stamina.CurrentVal = curStamina;
    }

    public void AdjustCurrentHealth(int h)
    {
        curHeatlh = (curHeatlh + h);

        if (curHeatlh <= 0)
        {
            curHeatlh = 0;
            dead = true;
        }
    }

    void AdjustCurrentMana(float h)
    {
        curMana = (curMana + h);

        if (curMana < 0)
        {
            curMana = 0;
        }
        if (curMana > maxMana)
        {
            curMana = maxMana;
        }
    }

    public void AdjusCurrentSprint(float s)
    {
        curStamina = (curStamina + s);

        if (curStamina < 0)
        {
            curStamina = 0;
            Fatigued = true;
        }
        else if (curStamina <= 10)
        {
            Fatigued = true;
        }
        if (curStamina == 50)
        {
            Fatigued = false;
        }

		if (curStamina > maxStamina)
		{
            curStamina = maxStamina;
		}
    }

}
