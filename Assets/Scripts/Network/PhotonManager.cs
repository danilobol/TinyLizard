﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour
{
    public static PhotonManager instance;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject lobbyCamera;
    [SerializeField]
    private GameObject MapGenerator;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        PhotonNetwork.ConnectUsingSettings("1.0");
    }

    void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions() { MaxPlayers = 2 }, TypedLobby.Default);
    }

    void OnJoinedRoom()
    {
        lobbyCamera.SetActive(false);

        GameObject p = PhotonNetwork.Instantiate ("Player", player.transform.position, Quaternion.identity,0);

        PhotonView ph = p.GetComponent<PhotonView>();
        Debug.Log("ph.viewID: " + ph.viewID);
        if(ph.viewID == 1001)
        {
            Debug.Log("Entrou");
            PhotonNetwork.Instantiate("MapGenerator", MapGenerator.transform.position, Quaternion.identity, 0);
        }

    }

    public void NPCInstances(string npcName, Vector3 pos)
    {
        
        PhotonNetwork.Instantiate(npcName, pos, Quaternion.identity, 0);
    }

}
