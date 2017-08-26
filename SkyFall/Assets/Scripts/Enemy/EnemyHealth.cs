using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    #region Variaveis
    public float maxHeatlh;
    public float curHeatlh;
    public float maxMana;
    public float curMana;
    public float expToGive;

    public bool dead = false;
    public bool GetDamage = false;
	
    public GameObject UI;
    public GameObject Indicator;
	public Targets tagset;
    public LevelSystem level;
    public EnemyAI ai;

    [SerializeField]
    private Stats _health;
    [SerializeField]
    private Stats _mana;
    [SerializeField]
    private Stats bg;
    #endregion

    public int curAimY = 200;
    
    private void Awake()
    {
        _health.Initialize();
        _mana.Initialize();
    }

    void Start() 
	{
        FloatingTextControler.Initialize();
    }

    void Update()
    {
        _health.MaxValue = maxHeatlh;
        _mana.MaxValue = maxMana;
        _health.CurrentVal = curHeatlh;
        _mana.CurrentVal = curMana;
    }

    public void AdjustCurrentHealth (int h)
	{
		curHeatlh = (curHeatlh + h);

		if (curHeatlh <= 0)
		{
			curHeatlh = 0;
            Dead();
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

    void Dead()
	{
        /*
        level.exp += (int)expToGive;
        Debug.Log(level.exp + ":" + level.curexp);        
        //tagset.DeselctedTarget ();
		//tagset.EnemyDead (gameObject);
        */
	}


    void OnTriggerEnter(Collider target)
    {
        if (target.tag == "Weapon")
        {
            //tagset.DealDamage(this.gameObject);
            AdjustCurrentHealth(-10);
            Debug.Log("bateu em algo");
            GetDamage = true;
        }
    }
}
