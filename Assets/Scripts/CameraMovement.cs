using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private float smoothTime;
    private float maxSmoothTime = 0.525f;
    private float minSmoothTime = 0f;
    private float targetsSpeed;
    private float maxSpeed = 50;
    private Vector3 positionVelocity = Vector3.zero;
    private bool ready = false;
    private float startTime;


    private void Awake()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Time.time - startTime >= 0.5) ready = true;
        if (!target || !ready) return;

        targetsSpeed = target.GetComponent<Rigidbody>().linearVelocity.magnitude;
        smoothTime = Mathf.Lerp(maxSmoothTime, minSmoothTime, targetsSpeed / maxSpeed);

        targetPosition = target.transform.position;
        targetRotation = target.transform.rotation;
        transform.SetPositionAndRotation(
            Vector3.SmoothDamp(transform.position, targetPosition, ref positionVelocity, smoothTime),
            Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / smoothTime)
        );
    }
}