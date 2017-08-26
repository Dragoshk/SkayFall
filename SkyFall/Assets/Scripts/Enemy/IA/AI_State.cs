using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyIA))]
public class AI_State : MonoBehaviour
{
    #region Variables
    [Header("UI")]
    [SerializeField]
    private Stats _health;
    [SerializeField]
    private Stats _mana;

    [Header("Life")]
    public float maxHeatlh;
    public float curHeatlh;
    public float maxMana;
    public float curMana;

    [Header("AI")]
    public EnemyIA baseIA;
    public Animator anim;
    public GameObject UI;
    public Transform alertDebugCube;
    public bool dead;
    public bool run;
    public bool alert = true;
    public bool aim;
    public bool shooting;
    public bool canChase;

    public float walkSpeed = 1;
    public float runSpeed = 2;

    public float maxStance = 0.9f;
    public float minStance = 0.1f;

    public Vector3 destPosition;
    private bool moveToPosition;
    [System.NonSerialized]
    private float stopDistance;
    public float alertLevel;
    public float viewAngleLimit;
    #endregion

    void Awake()
    {
        _health.Initialize();
        _mana.Initialize();
    }

    void Start()
    {
        baseIA.agent.updateRotation = true;
        FloatingTextControler.Initialize();

        if (GetComponentInChildren<EnemieSigthSphere>())
        {
            GetComponentInChildren<EnemieSigthSphere>().gameObject.layer = 2;
        }
    }
    
    void Update()
    {
        _health.MaxValue = maxHeatlh;
        _mana.MaxValue = maxMana;
        _health.CurrentVal = curHeatlh;
        _mana.CurrentVal = curMana;
        Moviment();

        if (alert)
        {
            float scale = alertLevel * 0.05f;
            alertDebugCube.localScale = new Vector3(scale, scale, scale);
        }
    }

    #region AI_Estructure
    public void StopMoving()
    {
        moveToPosition = false;
    }

    public void Moviment()
    {
        if (moveToPosition)
        {
            baseIA.agent.SetDestination(destPosition);
            float distanceToTarget = Vector3.Distance(transform.position, destPosition);

            if (distanceToTarget <= stopDistance)
            {
                moveToPosition = false;
                run = false;
            }
        }

        HandleSpeed();
        HandleAiming();
        HandleAnimation();
        HandleStates();
    }

    private void HandleSpeed()
    {
        if (!run) baseIA.agent.speed = walkSpeed; else baseIA.agent.speed = runSpeed;
    }

    private void HandleAnimation()
    {
        Vector3 relativeDirection = (transform.InverseTransformDirection(baseIA.agent.desiredVelocity)).normalized;

        float animValue = relativeDirection.z;

        if (!run)
        {
            animValue = Mathf.Clamp(animValue, 0,0.5f);
        }

        anim.SetFloat("Forward", animValue, 0.3f,Time.deltaTime);
    }

    private void HandleStates()
    {

    }

    private void HandleAiming()
    {
        //To Do aiming animation hare!!!
        //anim.Play("sword_and_shield_slash_2");
    }

    public void MoveToPosition(Vector3 position)
    {
        moveToPosition = true;
        destPosition = position;
    }

    public void CallFunctionWithString(string functionIdentifier, float delay)
    {
        Invoke(functionIdentifier, delay);
    }

    void AlertPhase()
    {
        alert = !alert;
    }

    void ChangeRunState()
    {
        run = !run;
    }

    public void ChangeToNormal()
    {
        baseIA.ChangeAIBehavior("AI_State_Normal",0);
        alert = false;
        run = false;
    }

    public void ChangeToAler(Vector3 position)
    {
        alert = true;
        moveToPosition = true;
        baseIA.OnGoAlert(position);
    }
    #endregion

    public void AdjustCurrentHealth(int h)
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
            //Debug.Log("bateu em algo");
            Debug.Log(curHeatlh);
        }
    }
}
