using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerOnFloor : MonoBehaviour
{
    public Transform floor;
    public GameObject blackLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (blackLayer == null)
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            player.MoveOnPlayer(floor.position);
        }
    }
}
