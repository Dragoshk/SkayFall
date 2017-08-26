using UnityEngine;
using System.Collections;

public class CostumeDriver_Hands : MonoBehaviour
{
    public GameObject JNT_Driven_OBJ;
    public GameObject JNT_Driven_OBJ_WR_R;
    public GameObject JNT_Driven_OBJ_WR_L;
    public GameObject JNT_Driven_OBJ_AR_R;
    public GameObject JNT_Driven_OBJ_AR_L;

    public GameObject JNT_Driver_OBJ;
#region DriversR
    public GameObject JNT_Driver_OBJ_WR_R;
    #endregion

#region DriversL
    public GameObject JNT_Driver_OBJ_WR_L;
#endregion

    void Start()
    {

    }

    void Update()
    {
        JNT_Driven_OBJ.transform.parent = JNT_Driver_OBJ.transform.parent;
        JNT_Driven_OBJ_WR_R.transform.parent = JNT_Driver_OBJ_WR_R.transform.parent;
        JNT_Driven_OBJ_WR_L.transform.parent = JNT_Driver_OBJ_WR_L.transform.parent;
    }
}
