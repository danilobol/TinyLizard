using UnityEngine;

[RequireComponent(typeof(HealthBehaviour))]
public class DamageableBehaviour : MonoBehaviour
{
    public HealthBehaviour healthBehaviour;
    public CreatureType creatureType;

    [Header("Damage Restriction")]
    public bool hasDamageTypeRestriction = false;
    public Damage.DamageType damageTypeRestriction;

    public delegate void OnDamage(GameObject agressor, Damage damage);
    public event OnDamage OnDamageEvent;

    public void Damage(GameObject agressor, Damage damage)
    {
      
        healthBehaviour.health.Damage(damage.damage);

        NotifyDamage(agressor, damage);

    }

    public void NotifyDamage(GameObject agressor, Damage damage)
    {
        if (OnDamageEvent != null)
        {
            OnDamageEvent(agressor, damage);
        }
    }


}
