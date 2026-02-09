using UnityEngine;

public class Jump : MonoBehaviour
{
    private SpSetup setup;
    private bool collected = false;
    private float startTime;
    [SerializeField] private bool up;

    private void Awake()
    {
        setup = GameObject.Find("ScriptExecuter").GetComponent<SpSetup>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            if (!collected)
            {
                collected = true;
                setup.AddForce((up) ? Vector3.up : Vector3.down);
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
