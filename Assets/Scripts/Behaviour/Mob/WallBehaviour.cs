using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public CharacterStatus characterStatus = new CharacterStatus();
    public HealthBehaviour healthBehaviour;
    public DamageableBehaviour damageableBehaviour;
    public Mob mob;
    // Start is called before the first frame update
    void Start()
    {
        characterStatus.health = healthBehaviour.health;

        characterStatus.health.maxHp.Set(mob.maxHealth);
        characterStatus.health.hp.Set(mob.maxHealth);

        DamageableBehaviour damageableBehaviour = GetComponent<DamageableBehaviour>();
        damageableBehaviour.healthBehaviour = healthBehaviour;
        damageableBehaviour.creatureType = mob.creatureType;

       
    }

    private void Update()
    {
        if (healthBehaviour.health.hp.Get() <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}
