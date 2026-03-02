using Unity.VisualScripting;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private bool collected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            if (!collected)
            {
                collected = true;
                other.gameObject.GetComponent<CarActions>().collectedCps++;
            }
        }
    }
}
