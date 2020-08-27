using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public PlayerController player;
    public GameObject FocusCamera; //onde a camera vai focar
    public bool active = false;


    private void Update()
    {
        if(player != null)
        {
            if(active == false)
            {
                active = true;
                FocusCamera.transform.SetParent(player.transform);
            }

        }
        else
        {
            player = FindObjectOfType<PlayerController>();
        }
    }
}
