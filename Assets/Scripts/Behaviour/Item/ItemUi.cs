using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemUi : MonoBehaviour
{
    public Text itemPointsText;
   
    public void SetItemUi(int point)
    {
        Debug.Log("---------------------> ADD os: "+ point);
        itemPointsText.text = point.ToString();
    }
}
