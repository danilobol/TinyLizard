using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatusUI : MonoBehaviour
{

    public FloatBarUI health;


    public void SetMobBehaviour(HealthBehaviour healthBehaviour)
    {
        health.Set(
            healthBehaviour.health.hp,
            healthBehaviour.health.maxHp
        );

    }
}
