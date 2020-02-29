using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBrain : Brain
{
    private Vector3 homePosition;
    private MobBehaviour mobBehaviour;

    Neuron  n0, n1, n2, n3, n4, n5;

    public PatrolBrain(MobBehaviour mobBehaviour, Vector3 homePosition) : base()
    {
        this.mobBehaviour = mobBehaviour;
        this.homePosition = homePosition;
    }

    protected override Neuron Build()
    {
        // is inside home distance
        n0 = new Neuron(IsInsideHomeDistance);
        // go back home
        n1 = new Neuron(GoBackHome);
        // set walk destiny around
        n2 = new Neuron(RandomizeWalkDestiny);
        // wait move ends
        n3 = new Neuron(IsWalkingEnded);
        // start move delay
        n4 = new Neuron(StartMoveDelay);
        // wait move delay ends
        n5 = new Neuron(WaitMovementDelay);

        return n0;
    }

    public void SetHomePosition(Vector3 homePosition)
    {
        this.homePosition = homePosition;
    }
    // n0
    public Neuron IsInsideHomeDistance()
    {
        return Vector3.Distance(mobBehaviour.transform.position, homePosition) <= mobBehaviour.mob.maxHomeDistance ? n2 : n1;
    }
    // n1
    public Neuron GoBackHome()
    {
        mobBehaviour.locomotor.MoveTo(homePosition, true);
        mobBehaviour.locomotor.Move();
        return n3;
    }
    // n2
    public Neuron RandomizeWalkDestiny()
    {
        mobBehaviour.SetWalkDestiny(mobBehaviour.GetRandomPositionAround(mobBehaviour.mob.idleWalkDistance));
        mobBehaviour.locomotor.Move();
        return n3;
    }
    // n3
    public Neuron IsWalkingEnded()
    {
        return mobBehaviour.locomotor.DestinationDistance <= mobBehaviour.mob.size ? n4 : n3;
    }
    // n4
    public Neuron StartMoveDelay()
    {
        mobBehaviour.locomotor.isMoving = false;
        mobBehaviour.StartCoroutine(mobBehaviour.WaitMoveDelay(mobBehaviour.mob.moveDelay));
        return n5;
    }
    // n5
    public Neuron WaitMovementDelay()
    {
        return mobBehaviour.locomotor.canMove ? n0 : n5;
    }

    public override void Reset()
    {
        currentNeuron = n0;
    }
}
