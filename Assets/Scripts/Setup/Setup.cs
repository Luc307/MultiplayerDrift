using System;
using System.Collections;
using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnpoint;
    [SerializeField] private GameObject[] spawnpoints = new GameObject[5];


    private void SpawnPlayers()
    {
        Instantiate(player, spawnpoint.transform.position, spawnpoint.transform.localRotation);
        //Debug.LogError("SpawnPlayers methode needs to be updated to be dynamic with multiplayer");
    }
    private void ToggleSpawnpoints()
    {
        if (Data.singleplayer)
        {
            spawnpoint.SetActive(true);
            foreach (GameObject spawnpoint in spawnpoints)
            {
                spawnpoint.SetActive(false);
            }
        }
        else
        {
            spawnpoint.SetActive(false);
            foreach (GameObject spawnpoint in spawnpoints)
            {
                spawnpoint.SetActive(true);
            }
        }
    }
    private void StartLevel()
    {
        Destroy(spawnpoint, 6);
        foreach(GameObject spawnpoint in spawnpoints)
        {
            Destroy(spawnpoint, 6);
        }
    }

    private void Awake()
    {
        ToggleSpawnpoints();
    }
    private void Start()
    {
        SpawnPlayers();
        StartLevel();
    }
}