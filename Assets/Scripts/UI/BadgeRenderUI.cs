using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeRenderUI : MonoBehaviour
{
    public Transform point;


    void OnDrawGizmos()
    {
        Vector3 posPoints = Camera.main.WorldToScreenPoint(this.transform.position);
        point.position = posPoints;
    }

    void OnGUI()
    {
        Vector3 posPoints = Camera.main.WorldToScreenPoint(this.transform.position);
        point.position = posPoints;
    }

    private void Start()
    {
        Vector3 posPoints = Camera.main.WorldToScreenPoint(this.transform.position);
        point.position = posPoints;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posPoints = Camera.main.WorldToScreenPoint(this.transform.position);
        point.position = posPoints;

    }
}
