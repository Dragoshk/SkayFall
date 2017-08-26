using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Component CameraScript;
    public Troca_cam Troca;
	public float xSpeed;
	public float ySpeed;
	public float zoomSpeed = 0.1f;
	public float maxZoom = 20f;
	public float minZoom = 5f;
    public bool olhacam = true;

	public GameObject target;
	public Camera Cameraobj;
	public float targetDistance;

	void FixedUpdate () 
	{
        targetDistance = Vector3.Distance(transform.position, target.transform.position);
        float mouseWheel = -Input.GetAxis("Mouse ScrollWheel");
        //Ativa Olhar para mouse

        Cameraobj.GetComponent<CameraController>().enabled = true;
		Cameraobj.GetComponent<FollowCam>().enabled = false;

        if (mouseWheel > 0f && targetDistance < maxZoom)
        {
            targetDistance++;
        }

        else if (mouseWheel < 0f && targetDistance > minZoom)
        {
            targetDistance--;
        }

        transform.position = target.transform.position - targetDistance * transform.forward;

    }

    public void RotateCameraAndCharacter()
    {
        float mouseh = Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        float mousev = Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        Troca.olhacam = false;
        transform.RotateAround(target.transform.position, target.transform.up, mouseh / 14);
        transform.RotateAround(target.transform.position, transform.right, -mousev);
    }

    public void RotateCamera()
    {
        float mouseh = Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        float mousev = Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        Troca.olhacam = false;
        Cameraobj.GetComponent<FollowCam>().enabled = false;
        transform.RotateAround(target.transform.position, target.transform.up, mouseh);
        transform.RotateAround(target.transform.position, transform.right, -mousev);
    }
}
