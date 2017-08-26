using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	public float targetDistance;
    public CameraController Distance;
	public float distanceUp = 5f;
	public float smooth = 3f;
	private Transform follow;
	private Vector3 targetPosition;

	public GameObject target;

	void Start () 
	{
		follow = target.transform;
	}

	void LateUpdate()
	{
        targetDistance = Distance.targetDistance;

        targetPosition = follow.position + follow.up * distanceUp - follow.forward * targetDistance;
		Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
		Debug.DrawRay(follow.position, -1f * follow.forward * targetDistance, Color.blue);
		Debug.DrawLine(follow.position, targetPosition, Color.magenta);

		transform.position = Vector3.Lerp (transform.position, targetPosition,Time.deltaTime * smooth);

		transform.LookAt(follow);

	}
}
