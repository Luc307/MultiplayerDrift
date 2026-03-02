using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarActions : MonoBehaviour
{
    private int neededCps;
    public int collectedCps;

    private bool finished = false;
 
    private int jumpForce = 50000;
    private int speedUpForce = 35;
    
 
    public IEnumerator SpeedUp()
    {
        while (true)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * speedUpForce, ForceMode.Acceleration);
            yield return new WaitForFixedUpdate();
        }
    }
    public IEnumerator OnFinishTrigger()
    {
        if (collectedCps >= neededCps)
        {
            if (!finished)
            {
                finished = true;

                Debug.Log("You successfully finished and collected all checkpoints!");

                GetComponent<CarController>().enabled = false;

                yield return new WaitForSeconds(0.5f);
                GetComponent<Rigidbody>().AddForce(transform.forward * -30000, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.Log("You did not collect all the checkpoints!");
        }
    }
    public void AddForce(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir * jumpForce, ForceMode.Impulse);
    }

    private void Awake()
    {
        neededCps = GameObject.FindGameObjectsWithTag("Checkpoint").Length;

        GameObject.Find("CameraHolder").GetComponent<MyCamera>().target = gameObject;
    }
    private void Update()
    {
        if (transform.position.y < -50) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}