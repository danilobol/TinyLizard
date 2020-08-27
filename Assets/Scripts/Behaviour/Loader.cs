using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    private void Awake()
    {
        if (Completed.BoardManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }
}
