using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    private GameObject carInstance;

    private int neededCps;
    public int collectedCps;

    [SerializeField] private GameObject spawnpoint;

    [SerializeField] private GameObject smallCar;

    private bool finished = false;
 
    private int jumpForce = 50000;
    private int speedUpForce = 35;
    
 
    public IEnumerator SpeedUp()
    {
        while (true)
        {
            carInstance.GetComponent<Rigidbody>().AddForce(carInstance.transform.forward * speedUpForce, ForceMode.Acceleration);
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

                carInstance.GetComponent<CarController>().enabled = false;

                yield return new WaitForSeconds(0.5f);
                carInstance.GetComponent<Rigidbody>().AddForce(carInstance.transform.forward * -30000, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.Log("You did not collect all the checkpoints!");
        }
    }
    public void AddForce(Vector3 dir)
    {
        carInstance.GetComponent<Rigidbody>().AddForce(dir * jumpForce, ForceMode.Impulse);
    }

    private void Awake()
    {
        neededCps = GameObject.FindGameObjectsWithTag("Checkpoint").Length;

        carInstance = Instantiate(smallCar, spawnpoint.transform.position, spawnpoint.transform.rotation);
        GameObject.Find("CameraHolder").GetComponent<MyCamera>().target = carInstance;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (carInstance.transform.position.y < -50) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}