using System.Collections;
using UnityEngine;

public class Speed : MonoBehaviour
{
    private CarActions carScript;
    private bool collected = false;


    private IEnumerator SetCollectedFalse()
    {
        yield return new WaitForSeconds(5);
        collected = false;
    }
    private IEnumerator StopSeedUp()
    {
        yield return new WaitForSeconds(0.75f);
        carScript.StopCoroutine("SpeedUp");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            if (!collected)
            {
                collected = true;
                StartCoroutine("SetCollectedFalse");

                carScript = other.gameObject.GetComponent<CarActions>();
                carScript.StartCoroutine("SpeedUp");
                StartCoroutine("StopSeedUp");
            }
        }
    }
}
