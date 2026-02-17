using UnityEngine;

public class Finish : MonoBehaviour
{
    private SpSetup spSetup;

    private void Awake()
    {
        spSetup = GameObject.Find("ScriptExecuter").GetComponent<SpSetup>();
    }
    private void OnTriggerEnter(Collider other)
    {
        spSetup.StartCoroutine("OnFinishTrigger");
    }
}
