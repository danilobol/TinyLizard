using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeRenderUI : MonoBehaviour
{
    public Image pointsText;
    public bool init = false;

    private void Start()
    {
        StartCoroutine(Initiate());
    }


    // Update is called once per frame
    void Update()
    {
        if (init == true)
        {
            Vector3 posPoints = Camera.main.WorldToScreenPoint(this.transform.position);
            pointsText.transform.position = posPoints;
        }

    }

    IEnumerator Initiate()
    {
        yield return new WaitForSeconds(5f);
        init = true;

    }
}
