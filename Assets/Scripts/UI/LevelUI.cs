using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Text levelText;
    

    public void ShowLevelText(int level)
    {
        levelText.text = "Você está no nivel: "+level;
    }
}
