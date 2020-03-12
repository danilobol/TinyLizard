using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MobBehaviour
{
    public Detector enemyDetector;
    public float lastTimeAttack;

    public PatrolBrain patrolBrain;
    public ChaseBrain chaseBrain;

    public LayerMask attackLayer;
    private Transform enemy;

    public override void Initialization()
    {
        base.Initialization();

        CreateBrain();

        enemyDetector.Criteria = EnemyCriteria;
        enemyDetector.OnEnemyFound += EnemyDetector_OnEnemyFound;
        enemyDetector.OnEnemyOutOfRange += EnemyDetector_OnEnemyOutOfRange;
    }

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    // Pick up the item
    void PickUp()
    {
        Debug.Log("Picking up " + this.name);

        //Destroy(gameObject);    // Destroy item from scene
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
        PlayerController damageable = target.GetComponent<PlayerController>();
        return damageable != null;
    }

    private void CreateBrain()
    {
        patrolBrain = new PatrolBrain(this, transform.position);
        chaseBrain = new ChaseBrain(this, MeleeAttackBehaviour);

        patrolBrain.SetHomePosition(transform.position);

        brain = patrolBrain;
    }

    public void AttackBehaviour()
    {
        ShootTarget(enemyDetector.target.transform.position);
    }

    public void MeleeAttackBehaviour()
    {
        MeleeAttackTarget(enemyDetector.target.transform.position, attackLayer);
    }



    void Update()
    {
        if (brain != null)
            brain.Think();

     //   animator.SetBool("Attacking", isAttacking);
    }

    public override void SetDefaultBrain()
    {
        brain = patrolBrain;
        patrolBrain.Reset();
    }
}
