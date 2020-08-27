using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileBehaviour : MonoBehaviour
{
    [HideInInspector]
    public GameObject caster;
    public Projectile projectile;
    public GameObject explosionPrefab;
    [HideInInspector]
    public Rigidbody2D rg;
    public Vector3 direction;
    public Vector3 inertiaSpeed;
    public float destroyDelay = 0;
    public bool hit = false;

    public bool areaAttack = false;
    public float radiusHit;
    public Collider2D[] enemys;
    public LayerMask layer;


    private DamageableBehaviour damageableHit = null;



    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.gravityScale = 0;
        rg.freezeRotation = true;
    
    }

    public void Shoot(GameObject owner, Vector3 initialPosition, Vector3 direction)
    {
        this.caster = owner;
        this.direction = direction;

        if (owner != null)
            inertiaSpeed = owner.GetComponent<Rigidbody>().velocity;


        transform.position = initialPosition;
        //transform.rotation = Quaternion.Euler(direction);
        transform.LookAt(initialPosition + (direction * 10f));

        Destroy(gameObject, projectile.duration);
    }

    private void OnDestroy()
    {
        if (explosionPrefab != null && hit)
        {
            Explode();
            Destroy(Instantiate(explosionPrefab, transform.position, Quaternion.identity), 3);

        }
    }

    public void Update()
    {
        rg.velocity = (direction + inertiaSpeed) * projectile.speed * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollision(other.gameObject);
    }

    private void OnCollision(GameObject other)
    {
        
        DamageableBehaviour damageable = other.GetComponent<DamageableBehaviour>();

        ProjectileBehaviour otherProjectileBehaviour = other.GetComponent<ProjectileBehaviour>();


        if (otherProjectileBehaviour != null)
        {
            Explode();

            Destroy(gameObject);

        }
        else if (damageable != null && damageable.gameObject != caster)
        {
            damageable.Damage(caster, new Damage(projectile.damage));
            damageableHit = damageable;

            if (destroyDelay == 0)
            {
                hit = true;
                Explode();
                Destroy(gameObject);
            }
            else
            {
                hit = true;
                Explode();
                Destroy(gameObject, destroyDelay);
            }
        }
      /*  if (areaAttack)
        {
            Enemys();
        }
        */
    }

    public void Explode()
    {

      
    }

    private void Enemys()
    {
        float radius = radiusHit * Mathf.Max(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);
        // Get random enemy
        enemys = Physics2D.OverlapCircleAll(this.transform.position, 3f, layer);

        foreach (Collider2D enemy in enemys)
        {
            if (enemy != null)
            {
              /*  DamageableBehaviour damageable = enemy.GetComponent<DamageableBehaviour>();
                if (damageable != null)
                {
                    if (damageableHit != null)
                    {
                        if (damageable.gameObject != damageableHit.gameObject)
                        {
                            if (damageable.gameObject != caster)
                                damageable.Damage(caster, new Damage(projectile.damageElement, projectile.damageType, projectile.damage));
                        }
                    }
                    else
                    {
                        if (damageable.gameObject != caster)
                            damageable.Damage(caster, new Damage(projectile.damageElement, projectile.damageType, projectile.damage));
                    }
                }*/
            }

        }
        Array.Clear(enemys, 0, enemys.Length);

    }

}