using System.Collections.Generic;

public abstract class Brain
{
    public Neuron currentNeuron;

    protected Brain()
    {
        currentNeuron = Build();
    }

    protected abstract Neuron Build();

    public void Think()
    {
        currentNeuron = currentNeuron.Think();
    }

    public abstract void Reset();
}
