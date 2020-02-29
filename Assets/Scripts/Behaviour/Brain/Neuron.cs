using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron
{
    public delegate Neuron NeuralAction();
    public NeuralAction neuralAction { get; private set; }

    public Neuron()
    {
    }

    public Neuron(NeuralAction neuralAction)
    {
        this.neuralAction = neuralAction;
    }

    public Neuron Think()
    {
        return neuralAction();
    }
}
