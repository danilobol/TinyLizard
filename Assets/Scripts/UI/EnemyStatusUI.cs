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
    public GameObject attackMortalUI;
    private GameObject enemyObject;
    public GameObject buttonsAutoAttack;
    public GameObject disabledButtonsMessage;

    //Barra de ataque mortal
    public Image iconAttackMortalUI;
    public Text pointsExtraText;

    private bool showMessage = true;
    private PlayerController player;

 


    public void SetEnemy(int life, int force, int points, int initLife, Sprite icon, int pointsExtra, GameObject enemy)
    {
        CharacterStatus characterStatus = new CharacterStatus();
        HealthBehaviour healthBehaviour = new HealthBehaviour();
        enemyObject = enemy;
        characterStatus.health = healthBehaviour.health;

        characterStatus.health.maxHp.Set(initLife);
        characterStatus.health.hp.Set(life);


        health.Set(
            characterStatus.health.hp,
            characterStatus.health.maxHp

        );
        pointsText.text = points.ToString();
        pointsExtraText.text = pointsExtra.ToString();
        iconUI.sprite = icon;
        iconAttackMortalUI.sprite = icon;
        this.force.text = force.ToString();

    }

    public void ActiveFrame()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        frameEnemy.SetActive(true);
        showMessage = true;
        attackMortalUI.SetActive(true);
        buttonsAutoAttack.SetActive(true);
        disabledButtonsMessage.SetActive(false);
        if (player.automaticAttack == true)
        {
            disabledButtonsMessage.SetActive(true);
            buttonsAutoAttack.SetActive(false);
        }
    }

    public void DesableFrame()
    {
        showMessage = true;
        attackMortalUI.SetActive(true);
        buttonsAutoAttack.SetActive(true);
        frameEnemy.SetActive(false);
    }

    public void DesableOrActiveAttackMortal()
    {
        if(showMessage == true)
        {
            showMessage = false;
            attackMortalUI.SetActive(false);

        }
        else
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            showMessage = true;
            attackMortalUI.SetActive(true);
            buttonsAutoAttack.SetActive(true);
            disabledButtonsMessage.SetActive(false);
            if (player.automaticAttack == true)
            {
                disabledButtonsMessage.SetActive(true);
                buttonsAutoAttack.SetActive(false);
            }
        }

    }

    public void AcceptDeadlyAttack()
    {
        if(enemyObject != null)
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            Slime enemy = enemyObject.GetComponent<Slime>();
            enemy.Deadlyattack();
            player.ActivateDeadlyAttack(enemyObject.GetComponent<DamageableBehaviour>());
            DesableOrActiveAttackMortal();
            buttonsAutoAttack.SetActive(false);
            disabledButtonsMessage.SetActive(true);

        }

    }



}
