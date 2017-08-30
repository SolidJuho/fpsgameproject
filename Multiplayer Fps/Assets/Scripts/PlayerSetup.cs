﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera scenecamera;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        } else
        {
            scenecamera = Camera.main;
            if (scenecamera != null)
            {
                Camera.main.gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        if(scenecamera != null)
        {
            scenecamera.gameObject.SetActive(true);
        }    
    }
}
