using Unity.VisualScripting;
using UnityEngine;

public class CarPlane : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 offset; 

    void Update()
    {
        Vector3 targetPosition = GameObject.FindGameObjectWithTag("Car").transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
