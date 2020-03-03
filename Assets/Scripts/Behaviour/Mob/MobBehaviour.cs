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


    private MobStatusUI mobStatusUI;

    public Animator animator;

    public delegate void OnMobDeath(MobBehaviour mobBehaviour);
    public event OnMobDeath OnMobDeathEvent;


    public void Start()
    {
        mobStatusUI = GetComponentInChildren<MobStatusUI>();
        if (mobStatusUI != null)
            mobStatusUI.SetMobBehaviour(damageableBehaviour.healthBehaviour);
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
        Collider2D[] hitEnimies = Physics2D.OverlapCircleAll(this.transform.position, 0.5f, layer);

        foreach (Collider2D enemy in hitEnimies)
        {
            Debug.Log("Atacou: " + enemy.gameObject.name);
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



    private IEnumerator MobDie()
    {
        locomotor.isDestinationLock = true;
        canAttack = false;
        isAttacking = false;
        animator.SetTrigger("death");



        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.1f);
      //  if (dropItem != null)
      //      dropItem.DropItemGo();
        Destroy(gameObject);
    }
}
