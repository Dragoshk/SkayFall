using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> waypoints = new List<GameObject>();
    public State states;
    public float time;
    public float rotationTime;
    public float moveSpeed;
    public int randomNumber;

    public enum State { Idle,Moving}

    void Start()
    {

    }

    void Update()
    {

    }

    void Moviment()
    {
        Debug.Log(randomNumber);
        if (randomNumber == 1)
        {

        }
    }

    /*
    public float range;
    public float Speed;
    public GameObject player;
    public NavMeshAgent nav;
    public Animator anim;
    public bool alive = true;
    public States ai;
    public enum States { idle, chase, patrol, attak }

    void Start()
    {
        nav.speed = Speed;
    }

    void Moving()
    {
        if (alive)
        {
            anim.SetFloat("Speed",nav.velocity.magnitude);

            switch (ai)
            {
                case States.idle:
                    ai = States.patrol;
                    break;

                case States.patrol:
                    Patrolling();
                    break;

                case States.chase:
                    Chasing();
                    break;

                case States.attak:
                    Attaking();
                    break;
            }
        }
    }

    void Attaking()
    {       
        anim.Play("sword_and_shield_slash_2");
    }

    void Chasing()
    {
        nav.SetDestination(player.transform.position);
        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);

        if (range <= distanceToTarget)
        {
            ai = States.attak;
        }
        else
        {
            ai = States.chase;
        }
    }

    void Patrolling()
    {
        if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending)
        {
            Vector3 randomPos = Random.insideUnitSphere * 10f;
            NavMeshHit navhit;
            NavMesh.SamplePosition(transform.position + randomPos, out navhit, 10, NavMesh.AllAreas);
            nav.SetDestination(navhit.position);
        }
    }*/
}