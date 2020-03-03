using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    public Health health { private set; get; }

    public HealthBehaviour()
    {
        health = new Health();
    }
}
