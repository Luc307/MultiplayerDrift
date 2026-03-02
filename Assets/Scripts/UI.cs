using FishNet.Demo.AdditiveScenes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private List<Image> lights;
    [SerializeField] private Image lightBackground;


    private IEnumerator SetUpLights()
    {
        foreach (Image light in lights)
        {
            light.enabled = true;
        }
        lightBackground.enabled = true;

        yield return new WaitForSeconds(2.25f);

        int index = 0;
        while (true)
        {
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
                Console.WriteLine("Level Start");
            }

            else if (index == 6)
            {
                foreach (Image light in lights)
                {
                    Destroy(light);
                }
                Destroy(lightBackground);
                StopCoroutine("SetUpLights");
            }
            yield return new WaitForSeconds(0.75f);
        }
    }
    private void Start()
    {
        StartCoroutine("SetUpLights");
    }
}