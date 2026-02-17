using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpSetup : MonoBehaviour
{
    private int neededCps;
    public int collectedCps = 0;

    [SerializeField] private GameObject smallCar;
    [SerializeField] private GameObject midCar;
    [SerializeField] private GameObject bigCar;

    private GameObject spawnpoint;
    private GameObject carInstance;

    [SerializeField] private List<Image> lights;
    [SerializeField] private Image lightBackground;
    private int index = 0;

    private GameObject[] cageTiles;

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

    bool finished = false;
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
    private void StartLevel()
    {
        foreach (GameObject cageTile in cageTiles)
        {
            Destroy(cageTile);
        }
    }
    private void SetUpLights()
    {
        if(index == 0)
        {
            foreach (Image light in lights)
            {
                light.enabled = true;
            }
            lightBackground.enabled = true;
        }
        if (index <= 4)
        {
            lights[index].color = Color.red;
            index++;
        }
        else if (index == 5)
        {
            foreach (Image light in lights)
            {
                light.color = Color.limeGreen;
            }
            index++;
            StartLevel();
        }
        else if (index == 6)
        {
            foreach (Image light in lights)
            {
                Destroy(light);
            }
            Destroy(lightBackground);
            CancelInvoke("StartLights");
        }
    }

    private void Awake()
    {
        spawnpoint = GameObject.Find("Spawnpoint");
        cageTiles = GameObject.FindGameObjectsWithTag("CageTile");

        Dictionary<string, GameObject> nameCarDic = new Dictionary<string, GameObject>()
        {
            {"SmallCar", smallCar},
            {"midCar", midCar},
            {"BigCar", bigCar},
        };
        GameObject carInstance = Instantiate(
            nameCarDic[Data.carName],
            spawnpoint.transform.position,
            spawnpoint.transform.rotation);
        GameObject.Find("CameraHolder").GetComponent<CameraMovement>().target = carInstance;
        this.carInstance = carInstance;

        neededCps = GameObject.FindGameObjectsWithTag("Checkpoint").Length;

        foreach(Image light in lights)
        {
            light.enabled = false;
        }
        lightBackground.enabled = false;
    }
    private void Start()
    {
        InvokeRepeating("SetUpLights", 2.25f, 0.75f);
    }
    private void Update()
    {
        if (carInstance.transform.position.y < -50) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}