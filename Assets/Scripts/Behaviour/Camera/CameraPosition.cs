using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public PlayerController player;

    void Update()
    {

        if (player != null)
            transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
        else
            player = FindObjectOfType<PlayerController>();



    }
}
