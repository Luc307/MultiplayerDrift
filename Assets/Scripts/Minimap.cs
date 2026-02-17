using Unity.VisualScripting;
using UnityEngine;

public class CarPlane : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool rotation;
    [SerializeField] private Quaternion rotationOffset;
    private GameObject car;
    private Vector3 targetPosition;


    private void Start()
    {
        car = GameObject.FindGameObjectWithTag("Car");
    }
    private void Update()
    {
        if (!car) return;

        targetPosition = car.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (rotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, car.transform.rotation * rotationOffset, Time.deltaTime);
        }
    }
}
