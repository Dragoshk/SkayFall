using UnityEngine;
using System.Collections;

public class CostumeDriver_Shoes : MonoBehaviour
{
    [Header("Driven")]
    public GameObject JNT_Driven_OBJ;
    public GameObject JNT_Driven_OBJ_LG_R;
    public GameObject JNT_Driven_OBJ_LG_L;
    public GameObject JNT_Driven_OBJ_Kne_aR;
    public GameObject JNT_Driven_OBJ_Kne_aL;
    public GameObject JNT_Driven_OBJ_Kne_bR;
    public GameObject JNT_Driven_OBJ_Kne_bL;
    public GameObject JNT_Driven_OBJ_Foot_R;
    public GameObject JNT_Driven_OBJ_Foot_L;
    public GameObject JNT_Driven_OBJ_Ball_R;
    public GameObject JNT_Driven_OBJ_Ball_L;
    [Header("Driver")]
    public GameObject JNT_Driver_OBJ;
    public GameObject JNT_Driver_OBJ_LG_R;
    public GameObject JNT_Driver_OBJ_LG_L;
    public GameObject JNT_Driver_OBJ_Kne_aR;
    public GameObject JNT_Driver_OBJ_Kne_aL;
    public GameObject JNT_Driver_OBJ_Kne_bR;
    public GameObject JNT_Driver_OBJ_Kne_bL;
    public GameObject JNT_Driver_OBJ_Foot_R;
    public GameObject JNT_Driver_OBJ_Foot_L;
    public GameObject JNT_Driver_OBJ_Ball_R;
    public GameObject JNT_Driver_OBJ_Ball_L;
    public bool hidefoot;
    public bool hideLeg;

    void Start()
    {
        ////JNT_Driver_OBJ = GameObject.Find("JNT_Driver_OBJ_SpA");
        //if (JNT_Driven_OBJ_Kne_aR != null)
        //{
        //    //JNT_Driver_OBJ_Kne_aR = GameObject.Find("JNT_Driver_OBJ_KNE_aR");
        //    JNT_Driven_OBJ_Kne_aR.transform.parent = JNT_Driver_OBJ_Kne_aR.transform.parent;
        //}

        //if (JNT_Driver_OBJ_Kne_aL != null)
        //{
        //    //JNT_Driver_OBJ_Kne_aL = GameObject.Find("JNT_Driver_OBJ_KNE_aL");
        //    JNT_Driven_OBJ_Kne_aL.transform.parent = JNT_Driver_OBJ_Kne_aL.transform.parent;
        //}
        //JNT_Driver_OBJ_Kne_bR = GameObject.Find("JNT_Driver_OBJ_KNE_bR");
        //JNT_Driver_OBJ_Kne_bL = GameObject.Find("JNT_Driver_OBJ_KNE_bL");
        //JNT_Driver_OBJ_Foot_R = GameObject.Find("JNT_Driver_OBJ_Foot_R");
        //JNT_Driver_OBJ_Foot_L = GameObject.Find("JNT_Driver_OBJ_Foot_L");
        //JNT_Driver_OBJ_Ball_R = GameObject.Find("JNT_Driver_OBJ_Ball_R");
        //JNT_Driver_OBJ_Ball_L = GameObject.Find("JNT_Driver_OBJ_Ball_L");

        //JNT_Driven_OBJ.transform.parent = JNT_Driver_OBJ.transform.parent;
        //JNT_Driven_OBJ_Kne_bR.transform.parent = JNT_Driver_OBJ_Kne_bR.transform.parent;
        //JNT_Driven_OBJ_Kne_bL.transform.parent = JNT_Driver_OBJ_Kne_bL.transform.parent;
        //JNT_Driven_OBJ_Foot_R.transform.parent = JNT_Driver_OBJ_Foot_R.transform.parent;
        //JNT_Driven_OBJ_Foot_L.transform.parent = JNT_Driver_OBJ_Foot_L.transform.parent;
        //JNT_Driven_OBJ_Ball_R.transform.parent = JNT_Driver_OBJ_Ball_R.transform.parent;
        //JNT_Driven_OBJ_Ball_L.transform.parent = JNT_Driver_OBJ_Ball_L.transform.parent;
    }

    void Update()
    {
        JNT_Driven_OBJ.transform.position = JNT_Driver_OBJ.transform.position;

        if (JNT_Driven_OBJ_LG_R != null)
        {
            JNT_Driven_OBJ_LG_R.transform.position = JNT_Driver_OBJ_LG_R.transform.position;
            JNT_Driven_OBJ_LG_R.transform.eulerAngles = JNT_Driver_OBJ_LG_R.transform.eulerAngles;
        }

        if (JNT_Driven_OBJ_LG_L != null)
        {
            JNT_Driven_OBJ_LG_L.transform.position = JNT_Driver_OBJ_LG_L.transform.position;
            JNT_Driven_OBJ_LG_L.transform.eulerAngles = JNT_Driver_OBJ_LG_L.transform.eulerAngles;
        }

        if (JNT_Driven_OBJ_Kne_aR != null)
        {
            JNT_Driven_OBJ_Kne_aR.transform.position = JNT_Driver_OBJ_Kne_aR.transform.position;
            JNT_Driven_OBJ_Kne_aR.transform.eulerAngles = JNT_Driver_OBJ_Kne_aR.transform.eulerAngles;
        }

        if (JNT_Driven_OBJ_Kne_aL != null)
        {
            JNT_Driven_OBJ_Kne_aL.transform.position = JNT_Driver_OBJ_Kne_aL.transform.position;
            JNT_Driven_OBJ_Kne_aL.transform.eulerAngles = JNT_Driver_OBJ_Kne_aL.transform.eulerAngles;
        }

        JNT_Driven_OBJ_Kne_bR.transform.position = JNT_Driver_OBJ_Kne_bR.transform.position;
        JNT_Driven_OBJ_Kne_bL.transform.position = JNT_Driver_OBJ_Kne_bL.transform.position;
        JNT_Driven_OBJ_Foot_R.transform.position = JNT_Driver_OBJ_Foot_R.transform.position;
        JNT_Driven_OBJ_Foot_L.transform.position = JNT_Driver_OBJ_Foot_L.transform.position;
        JNT_Driven_OBJ_Ball_R.transform.position = JNT_Driver_OBJ_Ball_R.transform.position;
        JNT_Driven_OBJ_Ball_L.transform.position = JNT_Driver_OBJ_Ball_L.transform.position;

        JNT_Driven_OBJ.transform.eulerAngles = JNT_Driver_OBJ.transform.eulerAngles;
        JNT_Driven_OBJ_Kne_bR.transform.eulerAngles = JNT_Driver_OBJ_Kne_bR.transform.eulerAngles;
        JNT_Driven_OBJ_Kne_bL.transform.eulerAngles = JNT_Driver_OBJ_Kne_bL.transform.eulerAngles;
        JNT_Driven_OBJ_Foot_R.transform.eulerAngles = JNT_Driver_OBJ_Foot_R.transform.eulerAngles;
        JNT_Driven_OBJ_Foot_L.transform.eulerAngles = JNT_Driver_OBJ_Foot_L.transform.eulerAngles;
        JNT_Driven_OBJ_Ball_R.transform.eulerAngles = JNT_Driver_OBJ_Ball_R.transform.eulerAngles;
        JNT_Driven_OBJ_Ball_L.transform.eulerAngles = JNT_Driver_OBJ_Ball_L.transform.eulerAngles;
    }
}
