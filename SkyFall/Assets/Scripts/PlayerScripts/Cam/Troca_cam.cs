using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Troca_cam : MonoBehaviour
{
	public bool olhacam = false;
	public Camera Cameraobj;
    // Update is called once per frame
    void Update()
    {

     
            if (!olhacam)
                Cameraobj.GetComponent<CameraController>().enabled = true;
            else
            {
                Cameraobj.GetComponent<FollowCam>().enabled = true;
                Cameraobj.GetComponent<CameraController>().enabled = false;
            }
        }

    
}
