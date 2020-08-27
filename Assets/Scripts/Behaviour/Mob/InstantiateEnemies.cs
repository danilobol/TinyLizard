using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEnemies : MonoBehaviour
{
    public WallBehaviour[] walls;
    // Start is called before the first frame update
    void Start()
    {
       /* WallBehaviour[] walls = FindObjectsOfType<WallBehaviour>();
        for (int i = 0; i < 10; i++)
        {

            Instantiate(BoardManager.instance.boss[Random.Range(0, BoardManager.instance.boss.Length)], walls[Random.Range(0, walls.Length)].transform.position, Quaternion.identity);
        }
        Debug.Log("Instanciou");

    */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
