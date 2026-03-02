using UnityEngine;

public class Jump : MonoBehaviour
{
    private bool collected = false;
    private float startTime;
    [SerializeField] private bool up;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            if (!collected)
            {
                collected = true;
                other.gameObject.GetComponent<CarActions>().AddForce(up ? Vector3.up : Vector3.down);
                startTime = Time.time;
            }
        }
    }
    private void Update()
    {
        if(Time.time - startTime >= 0.5f)
        {
            collected = false;
        }
    }
}
