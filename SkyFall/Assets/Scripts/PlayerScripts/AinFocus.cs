using UnityEngine;
using System.Collections;

public class AinFocus : MonoBehaviour 
{
	public Transform target;
	public int turnSpeed;

	// Update is called once per frame
	void Update () 
	{
		transform.rotation = Quaternion.Slerp (transform.rotation,
                                               Quaternion.LookRotation(target.position - transform.position), 
                                               turnSpeed * Time.deltaTime);
	}
}
