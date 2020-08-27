using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatusUI : MonoBehaviour
{
    public FloatBarUI health;
    public Text force;
    public Image iconUI;
    public Text pointsText;
    public GameObject frameEnemy;
    


    public void SetEnemy(int life, int force, int points, int initLife, Sprite icon)
    {
        CharacterStatus characterStatus = new CharacterStatus();
        HealthBehaviour healthBehaviour = new HealthBehaviour();

        characterStatus.health = healthBehaviour.health;

        characterStatus.health.maxHp.Set(initLife);
        characterStatus.health.hp.Set(life);


        health.Set(
            characterStatus.health.hp,
            characterStatus.health.maxHp

        );
        pointsText.text = points.ToString();
        iconUI.sprite = icon;
        this.force.text = force.ToString();

    }

    public void ActiveFrame()
    {
        frameEnemy.SetActive(true);

    }

    public void DesableFrame()
    {
        frameEnemy.SetActive(false);

    }





}
