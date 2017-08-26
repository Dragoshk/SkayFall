using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSigthSphere : MonoBehaviour
{
    public EnemyIA enAi;
    public AI_State charStats;
    [SerializeField]
    List<GameObject> trakingTargets = new List<GameObject>();

    //void Start()
    //{
    //    enAi = GetComponent<EnemyIA>();
    //    charStats = GetComponent<AI_State>();
    //}

    // Update is called once per frame
    void Update()
    {
        //if (enAi.target == null)
        //{
        for (int i = 0; i < trakingTargets.Count; i++)
        {
            if (trakingTargets[i] != enAi.myTarget)
            {
                Vector3 direction = trakingTargets[i].transform.position - transform.position;
                float angleTowardsTarget = Vector3.Angle(transform.parent.forward, direction.normalized);

                if (angleTowardsTarget < charStats.viewAngleLimit)
                {
                    enAi.myTarget = trakingTargets[i];
                }
                else
                {
                    continue;
                }
            }
        }
        //}
    }

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            Debug.Log(target.gameObject.tag);
            if (!trakingTargets.Contains(target.gameObject))
            {
                trakingTargets.Add(target.gameObject);
                //target.
            }
        }
    }

    //void OnTriggerExit(Collider target)
    //{
    //    if (target.gameObject.tag == "Player")
    //    {
    //        if (trakingTargets.Contains(target.gameObject))
    //        {
    //            trakingTargets.Remove(target.gameObject);
    //        }
    //    }
    //}
}
