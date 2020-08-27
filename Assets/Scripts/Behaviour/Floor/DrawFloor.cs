using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFloor : MonoBehaviour
{
    public Renderer rend;
    public GameObject blackLayer;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {
        if(blackLayer == null)
            rend.material.color = Color.yellow;
    }
    
    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        if (blackLayer == null)
            rend.material.color = Color.white;
    }
}
