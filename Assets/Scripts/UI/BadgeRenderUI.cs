using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeRenderUI : MonoBehaviour
{
    public Image pointsText;
    /*

    void Start()
    {
        GameObject itemUI = FindObjectOfType<GameUIBehaviour>().itemUI;
        GameObject imgUI = Instantiate(itemUI, itemUI.transform.position, Quaternion.identity);
        imgUI.transform.SetParent(FindObjectOfType<GameUIBehaviour>().transform);
        Image img = imgUI.GetComponentInChildren<Image>();
        pointsText = img;
    }


    // Update is called once per frame
    void Update()
    {
        pointsText.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
    */
}
