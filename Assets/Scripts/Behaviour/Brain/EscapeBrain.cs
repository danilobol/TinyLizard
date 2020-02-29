using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBrain : Brain
{
    private Vector3 enemy;
    private MobBehaviour mobBehaviour;

    Neuron n0, n1, n2, n3, n4;

    public EscapeBrain(MobBehaviour mobBehaviour, Vector3 enemy) : base()
    {
        this.mobBehaviour = mobBehaviour;
        this.enemy = enemy;
    }

    protected override Neuron Build()
    {
        n0 = new Neuron(IsAThreat);
        n1 = new Neuron(EscapeEnemy);
        n2 = new Neuron(IsWalkingEnded);
        n3 = new Neuron(StartMoveDelay);
        n4 = new Neuron(WaitMovementDelay);


        return n0;
    }










    // n0
    public Neuron IsAThreat()
    {
        return Vector3.Distance(mobBehaviour.transform.position, enemy) <= 10f ? n1 : n0;
    }
    // n1
    public Neuron EscapeEnemy()
    {
        int xDir = 1;
        int zDir = 1;

        //edit comportamento do inimigo

        //verificar a posição x entre inimigo e o player
        if (Mathf.Abs(enemy.x - mobBehaviour.transform.position.x) < float.Epsilon)
        {
            zDir = enemy.z > mobBehaviour.transform.position.z ? 1 : -1;

        }
        else
        {
            xDir = enemy.x < mobBehaviour.transform.position.x ? -1 : 1;
        }

        
        mobBehaviour.locomotor.canMove = true;
        mobBehaviour.locomotor.MoveTo(new Vector3(mobBehaviour.transform.position.x + 20f * xDir, mobBehaviour.transform.position.y, mobBehaviour.transform.position.z + 20f * zDir), true);
        
        

        return n2;
    }

    // n2
    public Neuron IsWalkingEnded()
    {
        return mobBehaviour.locomotor.DestinationDistance <= 0.5f ? n3 : n2;
    }

    // n3
    public Neuron StartMoveDelay()
    {
        mobBehaviour.StartCoroutine(mobBehaviour.WaitMoveDelay(mobBehaviour.mob.moveDelay));
        return n4;
    }
    // n4
    public Neuron WaitMovementDelay()
    {
        return mobBehaviour.locomotor.canMove ? n0 : n4;
    }

    public override void Reset()
    {
        currentNeuron = n0;
        enemy = mobBehaviour.transform.position;
    }

    public void SetEnemyPosition(Vector3 enemy)
    {
        this.enemy = enemy;
    }
}
