using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSetup : MonoBehaviour
{
    public BoardManager boardManager;
    private GameObject[] floor;



    void Start()
    {
        boardManager = FindObjectOfType<BoardManager>();
        boardManager.roomSetupTransform = this.transform;
        boardManager.SetupSccene(1);
       // ObjectsRoom();

    }

    public void ObjectsRoom()
    {

        int obejctCount = Random.Range(boardManager.wallCount.mininum, boardManager.wallCount.maxnum);

        floor = GameObject.FindGameObjectsWithTag("floor");



        for (int i = 0; i < obejctCount; i++)
        {
            int num = Random.Range(floor.Length, 0);
            GameObject tileChoice = boardManager.wallTiles[Random.Range(0, boardManager.wallTiles.Length)];
            GameObject instance = Instantiate(tileChoice, floor[num].gameObject.transform.position, Quaternion.identity);
            instance.transform.SetParent(boardManager.boardHolder);
            floor[num].tag = "floorX";
        }
        floor = null;
    }
}
