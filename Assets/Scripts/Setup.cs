using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Setup : MonoBehaviour
{
    [SerializeField] private Image[] lights;
    [SerializeField] private Image lightBackground;

    private GameObject[] spawnpoints;


    private IEnumerator SetUpLights()
    {
        foreach (Image light in lights)
        {
            light.enabled = true;
        }
        lightBackground.enabled = true;

        yield return new WaitForSeconds(2.25f);

        int index = 0;

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
        }
    }
    private void StartLevel()
    {
        foreach (GameObject spawnpoint in spawnpoints)
        {
            Destroy(spawnpoint);
        }
    }

    private void Awake()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
    }
    private void Start()
    {
        StartCoroutine("SetUpLights");
    }
}