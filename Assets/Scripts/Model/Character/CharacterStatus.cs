using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus
{
    public Health health { get; set; }
    public ObservableAttribute<float> speed { get; private set; }
    public ObservableAttribute<float> speedMultiplier { get; set; }

    public CharacterStatus()
    {
        speed = new ObservableAttribute<float>();
        speedMultiplier = new ObservableAttribute<float>(1);
    }
}
