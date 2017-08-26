using UnityEngine;
using System.Collections;

public class MouseTargeting : MonoBehaviour
{
    public GameObject targetScript;
    public Targeting targetAux;

    // Use this for initialization
    void Start()
    {
        // target must be local player autority
        targetScript = GameObject.FindGameObjectWithTag("Target");
        targetAux = targetScript.GetComponent<Targeting>();
    }

    void OnMouseDown()
    {
        targetAux.closestTarget = this.gameObject;
        Debug.Log(targetAux.closestTarget);
        targetAux.SwapTarget();
    }
}
