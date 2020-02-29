using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBrain : Brain
{
    Neuron n0, n1, n2, n3;

    private GameObject target;
    private Locomotor locomotor;

    public FollowBrain (Locomotor locomotor, GameObject target)
    {
        this.locomotor = locomotor;
        this.target = target;
    }

    protected override Neuron Build()
    {
        n0 = new Neuron(CheckTargetIsNotNull);
        n1 = new Neuron(MoveToTarget);
        n2 = new Neuron(FindTarget);
        n3 = new Neuron(PickTarget);
        return n3;
    }
    // n0
    public Neuron CheckTargetIsNotNull()
    {
        return target != null ? n1 : n0;
    }
    // n1
    public Neuron MoveToTarget()
    {
		if(locomotor != null && target != null)
			locomotor.MoveTo(target.transform.position, true);
        return n2;
    }
    // n2
    public Neuron FindTarget()
    {
        if (target == null)
        {
            return n0;

        }
        return Vector3.Distance(locomotor.transform.position, target.transform.position) <= 0.5f ? n3 : n1;
    }
    // n3
    public Neuron PickTarget()
    {
        locomotor.isDestinationLock = false;
        locomotor.Stop();
        target = null;
        return n0;
    }

    public override void Reset()
    {
        currentNeuron = n0;
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
}