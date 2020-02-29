using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureBehaviour : Interactable
{
    public Brain brain { protected set; get; }

    public virtual void SetDefaultBrain()
    {
        brain.Reset();
    }

    public virtual void SetBrain(Brain brain)
    {
        if (this.brain != null)
            this.brain.Reset();

        this.brain = brain;
    }
}
