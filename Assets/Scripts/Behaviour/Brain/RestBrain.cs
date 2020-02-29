using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestBrain : Brain
{
    private Vector3 restPosition;
    private MobBehaviour mobBehaviour;

    Neuron n0, n1, n2, n3, n4;

    public RestBrain(MobBehaviour mobBehaviour, Vector3 restPosition) : base()
    {
        this.mobBehaviour = mobBehaviour;
        this.restPosition = restPosition;
    }

    protected override Neuron Build()
    {
        n0 = new Neuron(IsInsideHomeDistance);
        n1 = new Neuron(GoRest);
        n2 = new Neuron(IsWalkingEnded);
        n3 = new Neuron(DestionationPosition);;

        return n0;
    }

    public void SetRestPosition(Vector3 restPosition)
    {
        this.restPosition = restPosition;
    }
    // n0
    public Neuron IsInsideHomeDistance()
    {
        return (Vector3.Distance(mobBehaviour.transform.position, restPosition) <= mobBehaviour.mob.maxHomeDistance) && mobBehaviour.locomotor.canMove ? n2 : n1;
    }
    // n1
    public Neuron GoRest()
    {
        mobBehaviour.locomotor.MoveTo(restPosition, true);
        return n2;
    }
    
    // n2
    public Neuron IsWalkingEnded()
    {
        mobBehaviour.StartCoroutine(mobBehaviour.DestinationTime(mobBehaviour.mob.moveDelay+3f));
        return mobBehaviour.locomotor.DestinationDistance <= 0.5f ? n3 : n2;
    }
    // n3
    public Neuron DestionationPosition()
    {
        mobBehaviour.locomotor.canMove = false;
        return n0;
    }

   
    public override void Reset()
    {
        currentNeuron = n0;
    }



}
