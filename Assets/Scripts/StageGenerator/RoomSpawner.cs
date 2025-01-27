﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    /*
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door


    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;
    //private BoardManager boardManager;

    public float waitTime = 0.5f;

    void Start()
    {
       // boardManager = FindObjectOfType<BoardManager>();
        
        // Destroy(gameObject, waitTime);
        templates = FindObjectOfType<RoomTemplates>();

        GameObject bg = GameObject.FindGameObjectWithTag("beginMap");

        if (bg == null)
        {
            Invoke("Spawn", 0.01f);
        }

    }


    void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == 1)
            {
                boardManager.roomSetupTransform = this.transform;
                boardManager.SetupSccene(openingDirection);
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                boardManager.roomSetupTransform = this.transform;
                boardManager.SetupSccene(openingDirection);
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                boardManager.roomSetupTransform = this.transform;
                boardManager.SetupSccene(openingDirection);
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                boardManager.roomSetupTransform = this.transform;
                boardManager.SetupSccene(openingDirection);
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Debug.Log("Fim");
                GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
                for (int i = 0; i < 10; i++)
                {

                  //  Instantiate(BoardManager.instance.boss[Random.Range(0, BoardManager.instance.boss.Length)], walls[Random.Range(0, walls.Length - 10)].transform.position, Quaternion.identity);
                }
                Debug.Log("Instanciou Inimigos");
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }

*/
}
