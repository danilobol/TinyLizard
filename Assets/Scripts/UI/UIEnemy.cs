using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIEnemy : MonoBehaviour
{
    public Text lifeText;
    public Text forceText;
    public Text pointsText;
    public GameObject restartLevel;
    // Start is called before the first frame update
    void Start()
    {
        lifeText.text = "0";
        forceText.text = "0";
        
    }
    
    public void SetEnemy(int life, int force, int points )
    {
        lifeText.text = life.ToString();
        forceText.text = force.ToString();
        pointsText.text = points.ToString();
    }

    public void RestartLevel()
    {
        
        Completed.GameManager.instance.Restart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        //restartLevel.SetActive(false);
    }
    public void DieUi()
    {
        restartLevel.SetActive(true);
    }
}
