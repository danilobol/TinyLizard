using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(DamageableBehaviour))]
[RequireComponent(typeof(SphereCollider))]
public class ProjectileShooterBehaviour : MonoBehaviour
{
    public float attackSpeed = 2;
    private float lastAttackTime = 0;

 //   public List<DamageableBehaviour> damageables;
    public Projectile projectile;

    public void Start()
    {
    //    damageables = new List<DamageableBehaviour>();
    }

    public void Shoot(Vector3 direction)
    {
        GameObject p = new GameObject();
        ProjectileBehaviour behaviour = p.GetComponent<ProjectileBehaviour>();
		if(behaviour != null)
			behaviour.Shoot(gameObject, transform.position + new Vector3(0, 1f, 0), direction);

	}

	/*void Update()
    {
        if (damageables.Count > 0)
        {
            if (Time.time - lastAttackTime > attackSpeed)
            {
                if (damageables[0] != null)
                    damageables[0].Damage(gameObject, new Damage(projectile.damageElement, projectile.damageType, projectile.damage));

                lastAttackTime = Time.time;
            }
        }
    }
    
    public void OnTriggerEnter(Collider collider)
    {
        DamageableBehaviour damageable = collider.gameObject.GetComponent<DamageableBehaviour>();

        if (damageable != null)
            damageables.Add(damageable);
    }

    public void OnTriggerExit(Collider collider)
    {
        DamageableBehaviour damageable = collider.gameObject.GetComponent<DamageableBehaviour>();

        if (damageable != null)
            damageables.Remove(damageable);
    }
    */
}
