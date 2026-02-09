using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private SpSetup setup;
    private bool collected = false;

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
                setup.collectedCps++;
            }
        }
    }
}
