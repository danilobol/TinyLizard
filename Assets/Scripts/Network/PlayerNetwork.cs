﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
    [SerializeField]
    private GameObject playerCamera;
    [SerializeField]
    private MonoBehaviour[] scriptsToIgnore;

    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }

    void Initialize()
    {
        if (photonView.isMine)
        {

        }
        else
        {
            playerCamera.SetActive(false);
            foreach (MobBehaviour item in scriptsToIgnore)
            {
                if(item.enabled == true)
                    item.enabled = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
