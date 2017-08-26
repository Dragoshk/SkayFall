using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public bool creatPointOfInterest;
    public List<AI_State> affectedChars = new List<AI_State>();

    void Update()
    {
        if (creatPointOfInterest)
        {
            for (int i = 0; i < affectedChars.Count; i++)
            {
                affectedChars[i].ChangeToAler(transform.position);
            }

            creatPointOfInterest = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AI_State>())
        {
            if (!affectedChars.Contains(other.GetComponent<AI_State>()))
            {
                affectedChars.Add(other.GetComponent<AI_State>());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<AI_State>())
        {
            if (!affectedChars.Contains(other.GetComponent<AI_State>()))
            {
                affectedChars.Remove(other.GetComponent<AI_State>());
            }
        }
    }
}
