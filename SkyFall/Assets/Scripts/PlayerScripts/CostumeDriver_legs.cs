using UnityEngine;
using System.Collections;

public class CostumeDriver_legs : MonoBehaviour
{
    [Header("Driven")]
    public GameObject JNT_Driven_OBJ;
    public GameObject JNT_Driven_OBJ_LG_R;
    public GameObject JNT_Driven_OBJ_LG_L;
    [Header("Driver")]
    public GameObject JNT_Driver_OBJ;
    public GameObject JNT_Driver_OBJ_LG_R;
    public GameObject JNT_Driver_OBJ_LG_L;
    public bool HideMesh = false;

    void Start()
    {
        //JNT_Driver_OBJ = GameObject.Find("JNT_Driver_OBJ_SpA");
        //JNT_Driver_OBJ_LG_R = GameObject.Find("JNT_Driver_OBJ_LG_R");
        //JNT_Driver_OBJ_LG_L = GameObject.Find("JNT_Driver_OBJ_LG_L");

        //JNT_Driven_OBJ.transform.parent = JNT_Driver_OBJ.transform.parent;
        //JNT_Driven_OBJ_LG_R.transform.parent = JNT_Driver_OBJ_LG_R.transform.parent;
        //JNT_Driven_OBJ_LG_L.transform.parent = JNT_Driver_OBJ_LG_L.transform.parent;
    }

    void Update()
    {
        JNT_Driven_OBJ.transform.position = JNT_Driver_OBJ.transform.position;
        JNT_Driven_OBJ_LG_R.transform.position = JNT_Driver_OBJ_LG_R.transform.position;
        JNT_Driven_OBJ_LG_L.transform.position = JNT_Driver_OBJ_LG_L.transform.position;
        JNT_Driven_OBJ.transform.eulerAngles = JNT_Driver_OBJ.transform.eulerAngles;
        JNT_Driven_OBJ_LG_R.transform.eulerAngles = JNT_Driver_OBJ_LG_R.transform.eulerAngles;
        JNT_Driven_OBJ_LG_L.transform.eulerAngles = JNT_Driver_OBJ_LG_L.transform.eulerAngles;
    }
}
