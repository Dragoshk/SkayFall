using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

public class EnemyIA : MonoBehaviour
{
    #region General Variables
    [SerializeField]
    public GameObject myTarget;
    public float sightDistance = 10;
    Vector3 lastKnownPosition;

    //General Behavior
    public float atackRange;
    public float delayTillBehavior = 3;
    float _timerTillBehavior;

    //Alert Variables
    public bool onPatrol;
    public bool onChase;
    public int indexBehavior;
    public List<WaypointsBase> onAlertExtraBehaviors = new List<WaypointsBase>();

    bool lookAtPOI;
    bool initAlert;
    Vector3 pointOfInterest;

    //Check for each Way point
    bool _initCheck;
    bool _lookAtTarget;
    bool _overrideAnimation;

    //Wait time for way point
    public bool circularList;
    bool descendingLIst;
    float _waitTime;

    //Look Rootation
    Quaternion targetRoot;

    //Waypoints main
    bool goToPos;
    public int indexWayPoint;
    public List<WaypointsBase> waypoints = new List<WaypointsBase>();
    public string[] alertLogic;

    //States
    public AiStates aiStates;
    #endregion

    public enum AiStates
    {
        patrol,
        chase,
        alert,
        onAlertBehavior,
        hastarget,
        attack
    }

    //Components
    public NavMeshAgent agent;
    public AI_State charState;

    public EnemiesManager enManager;
    private bool canChase;

    void Start()
    {
        charState.alert = false;

        enManager = GameObject.Find("NetworkController").GetComponent<EnemiesManager>();
        enManager.AllEnemies.Add(charState);

        if (onPatrol)
        {
            canChase = true;
            enManager.EnemiesOnPatrol.Add(charState);
        }

        if (canChase)
        {
            enManager.EnemiesAvaliableToChase.Add(charState);
        }

        sightDistance = GetComponentInChildren<EnemieSigthSphere>().GetComponent<SphereCollider>().radius;
    }

    void Update()
    {
        StateMachine();
    }

    public void StateMachine()
    {
        switch (aiStates)
        {
            case AiStates.patrol:
                DecreaseAlertLevels();
                PatrolBehavior();
                TargetAvaliable();
                break;

            case AiStates.alert:
                AlertBehaviorMain();
                TargetAvaliable();
                break;

            case AiStates.onAlertBehavior:
                TargetAvaliable();
                OnAlertExtraBehavior();
                break;

            case AiStates.chase:
                TargetAvaliable();
                ChaseBehavior();
                break;

            case AiStates.hastarget:
                HasTargetBeravior();
                break;

            case AiStates.attack:
                AttackBehavior();
                break;
        }
    }

    void HasTargetBeravior()
    {
        charState.StopMoving();

        if (sightRayCast())
        {
            if (charState.alertLevel < 10)
            {
                float distanceToTarget = Vector3.Distance(transform.position, myTarget.transform.position);
                float multiplier = 1 + (distanceToTarget / 0.1f);

                alertTimer += Time.deltaTime * multiplier;

                if (alertTimer > alertTimerIncrement)
                {
                    charState.alertLevel++;
                    alertTimer = 0;
                }
            }
            else
            {
                ChangeAIBehavior("AI_State_Attack", 0);
            }

            LookAtTarget(lastKnownPosition);
        }
        else
        {
            if (charState.alertLevel > 5)
            {
                ChangeAIBehavior("AI_State_Chase", 0);
                pointOfInterest = lastKnownPosition;
            }
            else
            {
                _timerTillBehavior += Time.deltaTime;

                if (_timerTillBehavior > delayTillBehavior)
                {
                    ChangeAIBehavior("AI_State_Normal", 0);
                    _timerTillNewBehavior = 0;
                }
            }
        }
    }

    void TargetAvaliable()
    {
        if (myTarget)
        {
            if (sightRayCast())
            {
                ChangeAIBehavior("AI_State_HasTArget", 0);
            }
        }
    }

    private bool sightRayCast()
    {
        bool retVal = false;
        RaycastHit HitTowardsLowerBody;
        RaycastHit HitTowardsUpperBody;
        float raycastDistance = sightDistance + (sightDistance * 0.5f);
        Vector3 targetPosition = lastKnownPosition;

        if (myTarget)
        {
            targetPosition = myTarget.transform.position;
        }

        Vector3 raycastStart = transform.position + new Vector3(0, 1.6f, 0);
        Vector3 direction = targetPosition - raycastStart;

        Debug.DrawRay(raycastStart, direction + new Vector3(0, 1, 0));

        if (Physics.Raycast(raycastStart, direction + new Vector3(0, 1, 0), out HitTowardsLowerBody, raycastDistance))
        {
            if (!myTarget)
            {
                if (HitTowardsLowerBody.transform == myTarget.transform)
                {
                    retVal = true;
                }
            }
        }

        if (retVal == false)
        {
            direction += new Vector3(0, 1.6f, 0);

            if (Physics.Raycast(raycastStart, direction, out HitTowardsUpperBody, raycastDistance/*,excludeLayers*/))
            {
                if (!myTarget)
                {
                    if (HitTowardsLowerBody.transform == myTarget.transform)
                    {
                        retVal = true;
                    }
                }
            }
        }

        return retVal;
    }

    void AttackBehavior()
    {
        if (sightRayCast())
        {
            LookAtTarget(lastKnownPosition);
        }
        else
        {
            charState.aim = false;
            ChangeAIBehavior("AI_State_Chase", 0);
        }
    }

    float alertTimer;
    float alertTimerIncrement = 1;
    private int _timerTillNewBehavior;

    void DecreaseAlertLevels()
    {
        if (charState.alertLevel > 0)
        {
            alertTimer += Time.deltaTime;
            if (alertTimer > alertTimerIncrement * 2)
            {
                charState.alertLevel--;
                alertTimer = 0;
            }
        }
    }

    void LookAtTarget(Vector3 positionToLook)
    {
        Vector3 directionToLook = positionToLook - transform.position;
        directionToLook.y = 0;

        float angle = Vector3.Angle(transform.forward, directionToLook);

        if (angle > 0.1f)
        {
            targetRoot = Quaternion.LookRotation(directionToLook);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRoot, Time.deltaTime * 3);
        }
    }

    public void ChangeAIBehavior(string behavior, int delay)
    {
        Invoke(behavior, delay);
    }

    #region Ai States
    void AI_State_Normal()
    {
        aiStates = AiStates.patrol;
        myTarget = null;
        charState.alert = false;
        goToPos = false;
        _lookAtTarget = false;
        _initCheck = false;
    }

    void AI_State_Chase()
    {
        aiStates = AiStates.chase;
        goToPos = false;
        _lookAtTarget = false;
        _initCheck = false;
    }

    void AI_State_OnAler_RumListOfBehaviors()
    {
        aiStates = AiStates.onAlertBehavior;
        charState.run = true;
        goToPos = false;
        _lookAtTarget = false;
        _initCheck = false;
    }

    void AI_State_HasTArget()
    {
        aiStates = AiStates.hastarget;
        charState.alert = true;
        goToPos = false;
        _lookAtTarget = false;
        _initCheck = false;
    }

    void AI_State_Attack()
    {
        aiStates = AiStates.attack;
    }
    #endregion

    public void OnGoAlert(Vector3 poi)
    {
        this.pointOfInterest = poi;
        aiStates = AiStates.alert;
        lookAtPOI = false;
    }

    public void AlertBehaviorMain()
    {
        if (!lookAtPOI)
        {
            Vector3 directionToLookTo = pointOfInterest - transform.position;
            directionToLookTo.y = 0;

            float angle = Vector3.Angle(transform.forward, directionToLookTo);

            if (angle > 0.1f)
            {
                targetRoot = Quaternion.LookRotation(directionToLookTo);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRoot, Time.deltaTime * 3);
            }
            else
            {
                lookAtPOI = true;
            }

            _timerTillBehavior += Time.deltaTime;

            if (_timerTillBehavior > delayTillBehavior)
            {
                if (alertLogic.Length > 0)
                {
                    ChangeAIBehavior(alertLogic[0], 0);
                }

                _timerTillBehavior = 0;
            }
        }
    }

    public void OnAlertExtraBehavior()
    {
        if (onAlertExtraBehaviors.Count > 0)
        {
            WaypointsBase curBehavior = onAlertExtraBehaviors[indexBehavior];

            if (!goToPos)
            {
                charState.MoveToPosition(curBehavior.targetDestination.position);
                goToPos = true;
            }
            else
            {
                float distanceToTarget = Vector3.Distance(transform.position, curBehavior.targetDestination.position);

                if (distanceToTarget < atackRange)
                {
                    CheckWayPoint(curBehavior, 1);
                }
            }
        }
    }

    public void ChaseBehavior()
    {
        if (myTarget == null)
        {
            if (!goToPos)
            {
                charState.MoveToPosition(pointOfInterest);
                charState.run = true;
                goToPos = true;
            }
            else
            {
                charState.MoveToPosition(myTarget.transform.position);
                charState.run = true;
            }
        }

        if (sightRayCast())
        {
            if (myTarget)
            {
                lastKnownPosition = myTarget.transform.position;
                myTarget = null;
            }
        }
    }

    void PatrolBehavior()
    {
        if (waypoints.Count > 0)
        {
            WaypointsBase curWaypoint = waypoints[indexWayPoint];

            if (!goToPos)
            {
                charState.MoveToPosition(curWaypoint.targetDestination.position);
                goToPos = true;
            }
            else
            {
                float distanceToTarget = Vector3.Distance(transform.position, curWaypoint.targetDestination.position);

                if (distanceToTarget < 10)
                {
                    CheckWayPoint(curWaypoint, 0);
                }
            }
        }
    }

    void CheckWayPoint(WaypointsBase wp, int listCase)
    {
        #region InitCheck
        if (!_initCheck)
        {
            _lookAtTarget = wp.lookTowards;
            _overrideAnimation = wp.overrideAnimation;
            _initCheck = true;
        }
        #endregion

        if (!wp.stopList)
        {
            switch (listCase)
            {
                case 0:
                    WaitTimerForEachWP(wp, waypoints);
                    break;
                case 1:
                    WaitTimerForExtraBehaviors(wp, onAlertExtraBehaviors);
                    break;
            }
        }

        #region LookTowards
        if (_lookAtTarget)
        {
            float speedToRotate;

            if (wp.speedToLook < 0.1f)
            {
                speedToRotate = 2;
            }
            else
            {
                speedToRotate = wp.speedToLook;
            }

            Vector3 direction = wp.TargetToLookTo.position - transform.position;
            direction.y = 0;

            float angle = Vector3.Angle(transform.forward, direction);

            if (angle > 0.1f)
            {
                targetRoot = Quaternion.LookRotation(direction);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRoot, Time.deltaTime * wp.speedToLook);
            }
            else
            {
                _lookAtTarget = false;
            }
        }

        #endregion

        #region AnimationOverride
        if (_overrideAnimation)
        {
            if (wp.animationRoutines.Length > 0)
            {
                for (int i = 0; i < wp.animationRoutines.Length; i++)
                {
                    charState.CallFunctionWithString(wp.animationRoutines[i], 0);
                }
            }
            else
            {
                Debug.Log("error");
            }
            _overrideAnimation = false;
        }
        #endregion
    }

    void WaitTimerForEachWP(WaypointsBase wp, List<WaypointsBase> listOfWp)
    {
        if (listOfWp.Count > 1)
        {
            #region WaitTime
            _waitTime += Time.deltaTime;

            if (_waitTime > wp.waitTime)
            {
                if (circularList)
                {
                    if (listOfWp.Count - 1 > indexWayPoint)
                    {
                        indexWayPoint++;
                    }
                    else
                    {
                        indexWayPoint = 0;
                    }
                }
                else
                {
                    if (!descendingLIst)
                    {
                        if (listOfWp.Count - 1 == indexWayPoint)
                        {
                            descendingLIst = true;
                            indexWayPoint--;
                        }
                        else
                        {
                            indexWayPoint++;
                        }
                    }
                }

                _initCheck = false;
                goToPos = false;
                _waitTime = 0;
            }
            #endregion
        }
    }

    void WaitTimerForExtraBehaviors(WaypointsBase wp, List<WaypointsBase> listOfWp)
    {
        if (listOfWp.Count > 1)
        {
            #region WaitTime
            _waitTime += Time.deltaTime;

            if (_waitTime > wp.waitTime)
            {
                if (circularList)
                {
                    if (listOfWp.Count - 1 > indexBehavior)
                    {
                        indexBehavior++;
                    }
                    else
                    {
                        indexBehavior = 0;
                    }
                }
                else
                {
                    if (!descendingLIst)
                    {
                        if (listOfWp.Count - 1 == indexBehavior)
                        {
                            descendingLIst = true;
                            indexBehavior--;
                        }
                        else
                        {
                            indexBehavior++;
                        }
                    }
                }

                _initCheck = false;
                goToPos = false;
                _waitTime = 0;
            }
            #endregion
        }
    }
}

[System.Serializable]
public struct WaypointsBase
{
    public Transform targetDestination;
    public float waitTime;
    public bool lookTowards;
    public Transform TargetToLookTo;
    public float speedToLook;
    public bool overrideAnimation;
    public string[] animationRoutines;
    public bool stopList;
}
