using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStatusUI : MonoBehaviour
{

    public FloatBarUI health;


    public void SetMobBehaviour(CharacterStatus characterStatus)
    {
        health.Set(
            characterStatus.health.hp,
            characterStatus.health.maxHp
        );

    }
}
