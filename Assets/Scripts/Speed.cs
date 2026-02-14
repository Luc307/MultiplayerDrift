using System.Collections;
using UnityEngine;

public class Speed : MonoBehaviour
{
    private SpSetup setup;
    private bool collected = false;


    private IEnumerator SetCollectedFalse()
    {
        yield return new WaitForSeconds(5);
        collected = false;
    }
    private IEnumerator StopSeedUp()
    {
        yield return new WaitForSeconds(0.75f);
        setup.StopCoroutine("SpeedUp");
    }

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
                StartCoroutine("SetCollectedFalse");

                setup.StartCoroutine("SpeedUp");
                StartCoroutine("StopSeedUp");
            }
        }
    }
}
