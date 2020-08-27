using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBrain : Brain
{
    public GameObject target;
    public MobBehaviour mobBehaviour;
    private float lastTimeAttack = 2;

    public delegate void AttackBehaviour();

    Neuron n0, n1, n2, n3, n4;

    public FollowBrain(MobBehaviour mobBehaviour) : base()
    {
        this.mobBehaviour = mobBehaviour;
    }

    protected override Neuron Build()
    {
        // Make mob chase enemy target, returns true if the mob reached the target
        n0 = new Neuron(ChaseEnemy);
        // wait attack delay
        n1 = new Neuron(WaitAttackDelay);
        // Make mob start attack delay, returns true if delay ended
        n2 = new Neuron(StartAttackDurationTime);
        // Wait attack delay
        n3 = new Neuron(WaitAttackDurationTime);
        

        return n0;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    // n0
    public Neuron ChaseEnemy()
    {
        if (target == null)
            return n0;

        Vector3 enemyPos = target.transform.position;
        bool insideRange = Vector3.Distance(mobBehaviour.transform.position, enemyPos) <= mobBehaviour.mob.attackRange;

        if (!insideRange)
        {
            mobBehaviour.locomotor.MoveTo(enemyPos, true);
            //mobBehaviour.locomotor.Move();
        }
        else
        {
            mobBehaviour.locomotor.Stop();
            mobBehaviour.SetWalkDestiny(mobBehaviour.transform.position);
        }

        return insideRange ? n1 : n0;
    }
    // n1
    public Neuron WaitAttackDelay()
    {
        return Time.time - lastTimeAttack > 1 / mobBehaviour.mob.attackSpeed ? n0 : n1;
    }
    // n2
    public Neuron StartAttackDurationTime()
    {
        mobBehaviour.isAttacking = true;
        mobBehaviour.StartCoroutine(mobBehaviour.WaitAttackDelay(mobBehaviour.mob.attackDurationTime));
        return n3;
    }
    // n3
    public Neuron WaitAttackDurationTime()
    {
        return mobBehaviour.locomotor.canMove ? n4 : n3;
    }
    

    public override void Reset()
    {
        target = null;
        currentNeuron = n0;
    }
}