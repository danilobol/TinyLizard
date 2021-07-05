using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MobBehaviour
{
    public int monsterAttack;
    public int health;
    public int points;
    private int initHealth;
    private Sprite icon;
    private bool slectEnemy = false;
    private PlayerController playerController;



    DamageableBehaviour damageable;

    public override void Initialization()
    {
        initHealth = health;
        icon = GetComponentInChildren<SpriteRenderer>().sprite;
        healthBehaviour = GetComponent<HealthBehaviour>();
        healthBehaviour.health.hp.Set((float) health);
        healthBehaviour.health.maxHp.Set((float)health);
        playerController = FindObjectOfType<PlayerController>();
        pointsExtra = (int)((points + initHealth) / monsterAttack);
        damageable = GetComponent<DamageableBehaviour>();
       // StartMob(health);
        damageable.OnDamageEvent += Damageable_OnDamageEvent;
        base.Initialization();

    }

    private void Damageable_OnDamageEvent(GameObject agressor, Damage damage)
    {
        if (healthBehaviour.health.hp.Get() > 0) {
            DamageableBehaviour damageableAgressor = agressor.GetComponent<DamageableBehaviour>();
            Damage dam = new Damage();
            dam.damage = monsterAttack;
            damageableAgressor.Damage(this.gameObject, dam);
            health = (int)healthBehaviour.health.hp.Get();
            animator.SetTrigger("Attack");
            SetEnemyUi();
        }
    }
    public void SetEnemyUi()
    {
        EnemyStatusUI ui = FindObjectOfType<EnemyStatusUI>();
        if(automaticAttack == false)
            pointsExtra = (int)((points + health) / monsterAttack);
        ui.ActiveFrame();
        ui.SetEnemy(health, monsterAttack, points, initHealth, icon, pointsExtra, this.gameObject);
    }

    void Update()
    {
        if (healthBehaviour.health.hp.Get() <= 0) {
            StartCoroutine(MobDie(points));
            EnemyStatusUI ui = FindObjectOfType<EnemyStatusUI>();
            ui.DesableFrame();
        }

        if(playerController.target != this.gameObject)
        {
           // slectEnemy = false;
        }

        //   animator.SetBool("Attacking", isAttacking);
    }

    private void OnMouseDown()
    {
        if(playerController.automaticAttack == false)
        {
            if (slectEnemy == true)
            {
                MovePlayerToEnemy();
            }
            SetEnemyUi();
            playerController.target = this.gameObject;
            slectEnemy = true;
        }
       
    }

    private void MovePlayerToEnemy()
    {
        //PlayerController play = GameObject.FindObjectOfType<PlayerController>();
        playerController.MoveOnPlayer(transform.position, this.gameObject);

    }
    void OnMouseExit()
    {
        slectEnemy = false;
    }

    public void Deadlyattack()
    {
        automaticAttack = true;
    }

}
