using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SigleplayerMenu : MonoBehaviour
{
    [SerializeField] private Button nextCar;
    [SerializeField] private Button prevCar;
    [SerializeField] private GameObject carFolder;
    private List<GameObject> carList;
    private Quaternion targetCarFolderRotation;
    private Quaternion targetCarRotation = Quaternion.Euler(0, -120, 0);
    private bool carRotationReady = true;
    private int carIndex = 0;
    private float carStartTime;

    private GameObject[] cars;

    [SerializeField] private Button nextMap;
    [SerializeField] private Button prevMap;
    [SerializeField] private GameObject mapFolder;
    private Quaternion targetMapFolderRotation;
    private bool mapRotationReady = true;
    private int mapIndex = 0;
    private float mapStartTime;
    private int mapCount = 7;

    private float rotationSpeed = 2.5f;

    [SerializeField] private Button play;


    private GameObject GetCar()
    {
        int index = (carIndex >= 0) ? carIndex : carIndex * -1;
        index = index % cars.Length;
        return cars[index];
    }
    private int GetMapIndex()
    {
        return (mapIndex % mapCount + mapCount) % mapCount;
    }
    private void ResetRotationReadyAfterTime()
    {
        if (!carRotationReady)
        {
            if (Time.time - carStartTime >= 0.25f)
            {
                carRotationReady = true;
            }
        }
        if (!mapRotationReady)
        {
            if (Time.time - mapStartTime >= 0.25f)
            {
                mapRotationReady = true;
            }
        }
    }
    private void ConfigListener(ref bool ready, ref Quaternion targetFolderRotation, float y, ref int index, bool indexUp, ref float startTime)
    {
        if (ready)
        {
            ready = false;
            startTime = Time.time;
            targetFolderRotation *= Quaternion.Euler(0, y, 0);
            index += indexUp ? 1 : -1;
        }
    }
    private void ButtonSetUp()
    {
        nextCar.onClick.AddListener(() =>
        {
            ConfigListener(ref carRotationReady, ref targetCarFolderRotation, 90, ref carIndex, indexUp: true, ref carStartTime);
        });
        prevCar.onClick.AddListener(() =>
        {
            ConfigListener(ref carRotationReady, ref targetCarFolderRotation, -90, ref carIndex, indexUp: false, ref carStartTime);
        });

        nextMap.onClick.AddListener(() =>
        {
            ConfigListener(ref mapRotationReady, ref targetMapFolderRotation, 51.43f, ref mapIndex, indexUp: true, ref mapStartTime);
        });
        prevMap.onClick.AddListener(() =>
        {
            ConfigListener(ref mapRotationReady, ref targetMapFolderRotation, -51.43f, ref mapIndex, indexUp: false, ref mapStartTime);
        });

        play.onClick.AddListener(() =>
        {
            Data.car = GetCar();
            Data.mapIndex = GetMapIndex();

            Debug.Log($"Map: {Data.mapIndex}, Car {Data.car}");

            SceneManager.LoadScene(mapIndex + 2);
        });
    }
    private void FolderRotation(GameObject folder, Quaternion targetFolderRotation)
    {
        folder.transform.rotation = Quaternion.Slerp(folder.transform.rotation, targetFolderRotation, Time.deltaTime * rotationSpeed);
    }
    private void CarRotation()
    {
        foreach (GameObject car in carList)
        {
            car.transform.rotation = Quaternion.Slerp(car.transform.rotation, targetCarRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void Awake()
    {
        targetCarFolderRotation = carFolder.transform.rotation;
        targetMapFolderRotation = mapFolder.transform.rotation;

        targetCarRotation = Quaternion.Euler(0, -120, 0);

        carList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Car"));
    }
    private void Start()
    {
        QualitySettings.shadowDistance = 300f;
        ButtonSetUp();
    }
    private void Update()
    {
        FolderRotation(carFolder, targetCarFolderRotation);
        FolderRotation(mapFolder, targetMapFolderRotation);
        CarRotation();
        ResetRotationReadyAfterTime();
    }
}