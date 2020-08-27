using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    public FloatBarUI health;
    public Text force;
    public Text level;


    public void SetCharacterBehaviour(CharacterStatus characterStatus)
    {
        health.Set(
            characterStatus.health.hp,
            characterStatus.health.maxHp
           
        );

    }

    private void Start()
    {
        level.text = Completed.GameManager.instance.GetLevel().ToString();
    }
    




}
