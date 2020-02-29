﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MobBehaviour
{
    public Detector enemyDetector;
    public float lastTimeAttack;

    public PatrolBrain patrolBrain;
    public ChaseBrain chaseBrain;

    private Transform enemy;

    public override void Initialization()
    {
        base.Initialization();

        CreateBrain();

        enemyDetector.Criteria = EnemyCriteria;
        enemyDetector.OnEnemyFound += EnemyDetector_OnEnemyFound;
        enemyDetector.OnEnemyOutOfRange += EnemyDetector_OnEnemyOutOfRange;
    }

    private void EnemyDetector_OnEnemyFound(GameObject target)
    {
        chaseBrain.Reset();
        chaseBrain.SetTarget(target);
        brain = chaseBrain;
    }

    private void EnemyDetector_OnEnemyOutOfRange(GameObject target)
    {
        patrolBrain.Reset();
        brain = patrolBrain;
    }

    public bool EnemyCriteria(GameObject target)
    {
        PlayerMovement damageable = target.GetComponent<PlayerMovement>();
        return damageable != null;
    }

    private void CreateBrain()
    {
        patrolBrain = new PatrolBrain(this, transform.position);
        chaseBrain = new ChaseBrain(this, AttackBehaviour);

        patrolBrain.SetHomePosition(transform.position);

        brain = patrolBrain;
    }

    public void AttackBehaviour()
    {
        ShootTarget(enemyDetector.target.transform.position);
    }



    void Update()
    {
        if (brain != null)
            brain.Think();

        animator.SetBool("Attacking", isAttacking);
    }

    public override void SetDefaultBrain()
    {
        brain = patrolBrain;
        patrolBrain.Reset();
    }
}
