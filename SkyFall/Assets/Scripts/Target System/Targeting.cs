using UnityEngine;
using System.Collections;

public class Targeting : MonoBehaviour
{
    public int turnSpeed;
    public bool targetactive = false;
    public GameObject[] Enamys;
    public GameObject closestTarget;
    public GameObject Self;
    public GameObject selectedTarget;
    public AI_State EH;
    Rigidbody theRigidBody;
    Vector3 myPosition;

    void Update()
    {
        if (selectedTarget != null)
        {
            EH = selectedTarget.GetComponent<AI_State>();
            selectedTarget.GetComponent<AI_State>().UI.SetActive(true);
            //EH.UI.transform.localRotation = Quaternion.Slerp(transform.rotation,
            //                                   Quaternion.LookRotation(Self.transform.position - transform.position),
            //                                   turnSpeed * Time.deltaTime);
            targetactive = true;

            if (EH.GetComponent<AI_State>().curHeatlh <= 0)
            {
                selectedTarget.GetComponent<AI_State>().UI.SetActive(false);
                Destroy(selectedTarget.gameObject);
            }
        }

        if (selectedTarget == null)targetactive = false;

        FollowTarget();
    }

    public void TargetSystemButton()
    {
        #region ClearVariables
        Enamys = null;
        closestTarget = null;
        #endregion

        #region localVariables
        Enamys = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = Self.GetComponent<Transform>().position;
        #endregion

        for (int i = 0; i < Enamys.Length; i++)
        {
            Vector3 diff = Enamys[i].transform.position - position;

            float curDistance = diff.sqrMagnitude;

            if ((curDistance < distance) && 
                (Enamys[i] != Self) && 
                (this.gameObject.transform.position != Enamys[i].transform.position))
            {
                distance = curDistance;
                closestTarget = Enamys[i];
            }
        }

        if (closestTarget != selectedTarget || selectedTarget == null)
        {
            SwapTarget();
        }
    }

    public void SwapTarget()
    {
        if (selectedTarget != null)
            selectedTarget.GetComponent<AI_State>().UI.SetActive(false);

        selectedTarget = closestTarget;
    }

    public void FollowTarget()
    {
        if (selectedTarget != null)
            this.gameObject.transform.position = selectedTarget.transform.position;
    }
}
