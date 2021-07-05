using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MobBehaviour : CreatureBehaviour
{
    [Header("Mob")]
    public Mob mob;
    
    public GameObject projectilePrefab = null;

    [Header("Mob Walking Behaviour")]
    public Locomotor locomotor;
    public Rigidbody2D myRigidbody;
    public Vector3 walkDestiny;
    public bool canAttack;
    public bool isAttacking;
    public DamageableBehaviour damageableBehaviour;
    public Projectile projectileMelee;

    public HealthBehaviour healthBehaviour;
    public CharacterStatus characterStatus = new CharacterStatus();

    [HideInInspector]
    public MobStatusUI mobStatusUI;

    public Animator animator;

    public delegate void OnMobDeath(MobBehaviour mobBehaviour);
    public event OnMobDeath OnMobDeathEvent;

    private bool mobStayDie = false;

    public int pointsExtra;

    //quando o player estiver com ataque automático isso é true
    public bool automaticAttack = false;

    public void Start()
    {
        Initialization();
    }

    public virtual void Initialization()
    {

        locomotor.speed = mob.speed;
    }

    

    public virtual void SetWalkDestiny(Vector3 pos)
    {
        locomotor.MoveTo(pos, true);
    }

    public Vector3 GetRandomPositionAround(float distance)
    {
        Vector3 pos = new Vector3(
                Random.Range(-distance, distance),
                0,
                Random.Range(-distance, distance)
        );

        pos += transform.position;
        return pos;
    }

    public void ShootTarget(Vector3 target)
    {
        if (projectilePrefab)
        {
            Vector3 max = this.GetComponent<BoxCollider>().bounds.max;
            StartCoroutine(InstantiateBullet(target));

        }
    }

    public void MeleeAttackTarget(Vector3 target, LayerMask layer)
    {
        Collider2D[] hitEnimies = Physics2D.OverlapCircleAll(this.transform.position, 3f, layer);

        foreach (Collider2D enemy in hitEnimies)
        {
            DamageableBehaviour damageable = enemy.GetComponent<DamageableBehaviour>();
            if(damageable != null && damageable.gameObject != this.gameObject)
            {
                StartCoroutine(DamageMob(damageable));
                
            }
        }
    }
    public IEnumerator WaitMoveDelay(float seconds)
    {
        locomotor.canMove = false;
        yield return new WaitForSeconds(seconds);
        locomotor.canMove = true;
    }

    public IEnumerator WaitAttackDelay(float seconds)
    {
        locomotor.canMove = false;
        canAttack = true;
        yield return new WaitForSeconds(seconds);
        locomotor.canMove = true;
        canAttack = false;
    }

    public IEnumerator DestinationTime(float seconds)
    {
        locomotor.canMove = true;
        yield return new WaitForSeconds(seconds);
        locomotor.canMove = false;
    }

    public IEnumerator InstantiateBullet(Vector3 target)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject projectileGO = Instantiate(projectilePrefab);

        Vector3 center = target - transform.position;
        float angle = AimAssist.FindDegree(center.x, center.z);

        Vector3 projectileStartPos = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad) * mob.size,
            0.5f,
            Mathf.Sin(angle * Mathf.Deg2Rad) * mob.size
        );

        ProjectileBehaviour pb = projectileGO.GetComponent<ProjectileBehaviour>();
        pb.Shoot(gameObject, transform.position + projectileStartPos, new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 360, 0, Mathf.Sin(angle * Mathf.Deg2Rad) * 360));

    }

   


    protected void OnMobDeathNotify()
    {
        if (OnMobDeathEvent != null)
            OnMobDeathEvent(this);
    }



    public IEnumerator MobDie(int points)
    {
        if (mobStayDie == false)
        {
            mobStayDie = true;
            locomotor.isDestinationLock = true;
            canAttack = false;
            isAttacking = false;
            animator.SetBool("Die", true);
            Completed.BoardManager.instance.RemoveEnemyList(this.gameObject);
            Completed.BoardManager.instance.enemyList.Remove(this.gameObject);
            DropItem dropItem = GetComponentInParent<DropItem>();
            if (automaticAttack == true)
                points += pointsExtra;
            dropItem.DropItemMob((float)points);
            PlayerController player = FindObjectOfType<PlayerController>();
            player.DisableDeadlyAttack();

            /*
            HealthBehaviour healt = FindObjectOfType<PlayerController>().GetComponent<HealthBehaviour>();

            if (healt.health.hp.Get() + points > healt.health.maxHp.Get())
            {
                if (healt.health.hp.Get() + points < 100)
                    healt.health.maxHp.Set(healt.health.hp.Get() + points);
                else
                {
                    healt.health.maxHp.Set(100);
                }
            }


            healt.health.Heal((float)points);
            */
        }
        /*
         * healthBehaviour.health.hp.Get()
         characterStatus.health.maxHp.Set(healt);
        characterStatus.health.hp.Set(healt);
         */

        //yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.1f);
        //  if (dropItem != null)
        //      dropItem.DropItemGo();
        if (mobStatusUI != null)
            Destroy(mobStatusUI.gameObject);
        Destroy(gameObject);
    }

    public void StartMob(float health)
    {
        characterStatus.health = healthBehaviour.health;

        characterStatus.health.maxHp.Set(health);
        characterStatus.health.hp.Set(health);

        DamageableBehaviour damageableBehaviour = GetComponent<DamageableBehaviour>();
        damageableBehaviour.healthBehaviour = healthBehaviour;
        damageableBehaviour.creatureType = mob.creatureType;

        if (mobStatusUI == null)
            mobStatusUI = GetComponentInChildren<MobStatusUI>();
        if (mobStatusUI != null)
            mobStatusUI.SetMobBehaviour(characterStatus);
    }


    IEnumerator DamageMob(DamageableBehaviour damageable)
    {
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 1f);
        damageable.Damage(this.gameObject, new Damage(projectileMelee.damage));

    }
   
}
