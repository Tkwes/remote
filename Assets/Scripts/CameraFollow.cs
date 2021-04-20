using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	Vector3 smoothVelocity;

    private void Start()
    {
		transform.parent = null;
		target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition,ref smoothVelocity, smoothSpeed);
		transform.position = smoothedPosition;

		transform.LookAt(target);
	}
}
