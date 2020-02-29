using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{

    public RoomTemplates templates;

    void Start()
    {
        if(templates == null)
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //templates.rooms.Add(this.gameObject);
    }
}
