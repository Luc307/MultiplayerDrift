using FishNet.Object;
using System;
using Unity.Entities;
using UnityEngine;

public class MpPlayerCamera : NetworkBehaviour
{
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private MyCamera cameraScript;

    public override void OnStartClient()
    {
        if (cameraHolder || cameraScript || IsOwner)
        {
            Instantiate(cameraHolder);
            cameraScript.target = gameObject;
        }
    }
}
