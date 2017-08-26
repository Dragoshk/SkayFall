using UnityEngine;
using System.Collections;

public class CostumeDriver_Chest : MonoBehaviour
{
    //Driven
    [Header("Driven")]
    public GameObject JNT_Driven_OBJ;
    public GameObject JNT_Driven_OBJ_SH_R;
    public GameObject JNT_Driven_OBJ_SH_L;
    public GameObject JNT_Driven_OBJ_AR_R;
    public GameObject JNT_Driven_OBJ_AR_L;
    //iff is not null
    public GameObject JNT_Driven_OBJ_ER_Ra;
    public GameObject JNT_Driven_OBJ_ER_La;
    public GameObject JNT_Driven_OBJ_ER_Rb;
    public GameObject JNT_Driven_OBJ_ER_Lb;
    //Driver
    [Header("Driver")]
    public GameObject JNT_Driver_OBJ;
    public GameObject JNT_Driver_OBJ_SH_R;
    public GameObject JNT_Driver_OBJ_SH_L;
    public GameObject JNT_Driver_OBJ_AR_R;
    public GameObject JNT_Driver_OBJ_AR_L;
    //iff is not null
    public GameObject JNT_Driver_OBJ_ER_Ra;
    public GameObject JNT_Driver_OBJ_ER_La;
    public GameObject JNT_Driver_OBJ_ER_Rb;
    public GameObject JNT_Driver_OBJ_ER_Lb;

    void Start ()
    {
    //    if (JNT_Driven_OBJ_ER_Ra != null)
    //    {
    //        JNT_Driven_OBJ_ER_Ra.transform.parent = JNT_Driver_OBJ_ER_Ra.transform.parent;
    //    }

    //    if (JNT_Driven_OBJ_ER_La != null)
    //    {
    //        JNT_Driven_OBJ_ER_La.transform.parent = JNT_Driver_OBJ_ER_La.transform.parent;
    //    }

    //    if (JNT_Driven_OBJ_ER_Rb != null)
    //    {
    //        JNT_Driven_OBJ_ER_Rb.transform.parent = JNT_Driver_OBJ_ER_Rb.transform.parent;
    //    }

    //    if (JNT_Driven_OBJ_ER_Lb != null)
    //    {
    //        JNT_Driven_OBJ_ER_Lb.transform.parent = JNT_Driver_OBJ_ER_Lb.transform.parent;
    //    }

    //    JNT_Driven_OBJ.transform.parent = JNT_Driver_OBJ.transform.parent;
    //    JNT_Driven_OBJ_SH_R.transform.parent = JNT_Driver_OBJ_SH_R.transform.parent;
    //    JNT_Driven_OBJ_SH_L.transform.parent = JNT_Driver_OBJ_SH_L.transform.parent;
    //    JNT_Driven_OBJ_AR_R.transform.parent = JNT_Driver_OBJ_AR_R.transform.parent;
    //    JNT_Driven_OBJ_AR_L.transform.parent = JNT_Driver_OBJ_AR_L.transform.parent;
    }

	void Update ()
    {
        JNT_Driven_OBJ.transform.position = JNT_Driver_OBJ.transform.position;
        JNT_Driven_OBJ_SH_R.transform.position = JNT_Driver_OBJ_SH_R.transform.position;
        JNT_Driven_OBJ_SH_L.transform.position = JNT_Driver_OBJ_SH_L.transform.position;
        JNT_Driven_OBJ_AR_R.transform.position = JNT_Driver_OBJ_AR_R.transform.position;
        JNT_Driven_OBJ_AR_L.transform.position = JNT_Driver_OBJ_AR_L.transform.position;

        if (JNT_Driver_OBJ_ER_Ra != null)
        {
            JNT_Driven_OBJ_ER_Ra.transform.position = JNT_Driver_OBJ_ER_Ra.transform.position;
            JNT_Driven_OBJ_ER_Ra.transform.eulerAngles = JNT_Driver_OBJ_ER_Ra.transform.eulerAngles;
        }

        if (JNT_Driver_OBJ_ER_La != null)
        {
            JNT_Driven_OBJ_ER_La.transform.position = JNT_Driver_OBJ_ER_La.transform.position;
            JNT_Driven_OBJ_ER_La.transform.eulerAngles = JNT_Driver_OBJ_ER_La.transform.eulerAngles;
        }

        if (JNT_Driver_OBJ_ER_Rb != null)
        {
            JNT_Driven_OBJ_ER_Rb.transform.position = JNT_Driver_OBJ_ER_Rb.transform.position;
            JNT_Driven_OBJ_ER_Rb.transform.eulerAngles = JNT_Driver_OBJ_ER_Rb.transform.eulerAngles;
        }

        if (JNT_Driver_OBJ_ER_Lb != null)
        {
            JNT_Driven_OBJ_ER_Lb.transform.position = JNT_Driver_OBJ_ER_Lb.transform.position;
            JNT_Driven_OBJ_ER_Lb.transform.eulerAngles = JNT_Driver_OBJ_ER_Lb.transform.eulerAngles;
        }

        JNT_Driven_OBJ.transform.eulerAngles = JNT_Driver_OBJ.transform.eulerAngles;
        JNT_Driven_OBJ_SH_R.transform.eulerAngles = JNT_Driver_OBJ_SH_R.transform.eulerAngles;
        JNT_Driven_OBJ_SH_L.transform.eulerAngles = JNT_Driver_OBJ_SH_L.transform.eulerAngles;
        JNT_Driven_OBJ_AR_R.transform.eulerAngles = JNT_Driver_OBJ_AR_R.transform.eulerAngles;
        JNT_Driven_OBJ_AR_L.transform.eulerAngles = JNT_Driver_OBJ_AR_L.transform.eulerAngles;
    }
}
