using UnityEngine;
using System.Collections;

public class Enemy_Patrol : MonoBehaviour
{
    public Transform[] Waypoints;
    public float Speed;
    public int curWayPoint;
    public bool doPatrol = true;
    public Vector3 Target;
    public Vector3 MoveDirection;
    public Vector3 Velocity;
    public Rigidbody rb;

    void Start()
    {

    }

    void Update()
    {
        rb = GetComponent<Rigidbody>();

        if (curWayPoint < Waypoints.Length)
        {
            Target = Waypoints[curWayPoint].position;
            MoveDirection = Target - transform.position;
            Velocity = rb.velocity;

            if (MoveDirection.magnitude < 1)
            {
                curWayPoint++;
            }
            else
            {
                Velocity = MoveDirection.normalized * Speed;
            }
        }
        else
        {
            if (doPatrol)
            {
                curWayPoint = 0;
            }
            else
            {
                Velocity = Vector3.zero;
            }
        }

        rb.velocity = Velocity;
        transform.LookAt(Target);
    }
}
