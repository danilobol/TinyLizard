using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDetector : MonoBehaviour
{
    public Collider2D[] collision;
    public GameObject wallObject;
    public LayerMask wallLayer;
    void Start()
    {
        collision = Physics2D.OverlapCircleAll(this.transform.position, 0.1f, wallLayer);
        foreach(Collider2D col in collision)
        {
            ObjectsDetector ob = col.GetComponentInChildren<ObjectsDetector>();
            if (ob != null && col.gameObject != this.gameObject)
            {
                Destroy(ob.wallObject);
            }
        }
    }
  
}
