using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeRenderUI : MonoBehaviour
{
    public Image pointsText;


    private void Start()
    {
        Vector3 posPoints = Camera.main.WorldToScreenPoint(this.transform.position);
        pointsText.transform.position = posPoints;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posPoints = Camera.main.WorldToScreenPoint(this.transform.position);
        pointsText.transform.position = posPoints;


    }
}
