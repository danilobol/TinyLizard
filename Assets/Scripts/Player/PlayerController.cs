using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HealthBehaviour))]
[RequireComponent(typeof(DamageableBehaviour))]



public class PlayerController : CreatureBehaviour
{
    [Header("Dependencies")]
    public Character character;
    public HealthBehaviour healthBehaviour;
    public DamageableBehaviour damageableBehaviour;
    public GameObject visual;


    public CharacterStatus characterStatus;

    public LayerMask groundedLayer;

    public Vector2 playerInput;
    Rigidbody2D rb;


    private bool initialized = false;
    private bool counterattacked = false;
    private float moveSpeed;
    private bool dead = false;

    public PlayerController()
    {
        characterStatus = new CharacterStatus();
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Init();
    }

    public void Init()
    {
        if (initialized)
            return;

        characterStatus.health = healthBehaviour.health;

        characterStatus.health.maxHp.Set(character.baseHealth);
        characterStatus.health.hp.Set(character.baseHealth);

        moveSpeed = character.baseSpeed;

        DamageableBehaviour damageableBehaviour = GetComponent<DamageableBehaviour>();
        damageableBehaviour.creatureType = character.creatureType;

      
        initialized = true;
    }

  




    private void Update()
    {

        if (Input.GetKey(KeyCode.E) && counterattacked == false)
        {
            float radius = 3f * Mathf.Max(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);
            // Get random enemy
            Collider[] proj = Physics.OverlapSphere(this.transform.position, radius);
            foreach (Collider pb in proj)
            {
                ProjectileBehaviour projectileBehaviour = pb.GetComponent<ProjectileBehaviour>();
                if (projectileBehaviour != null)
                {
                    Counterattack(projectileBehaviour);
                }

            }
            counterattacked = true;
            StartCoroutine(Counterattacked());
        }
        if (brain != null)
            brain.Think();
        if (healthBehaviour.health.hp.Get() <= 0f)
        {
            StartCoroutine(Die());

        }
    }
    void FixedUpdate()
    {
        ShowGroung();
        SetEnemyDead();

        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = playerInput.normalized * moveSpeed;
    }


    public override void SetDefaultBrain()
    {
        brain = null;
    }

   
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Morreu!");
        Destroy(this.gameObject);
        //SceneManager.LoadScene("Menu");
    }

    private void Counterattack(ProjectileBehaviour pb)
    {
        Vector3 center = pb.caster.transform.position - transform.position;
        float angle = AimAssist.FindDegree(center.x, center.z);
        Vector3 projectileStartPos = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad) * 0.2f,
            0.5f,
            Mathf.Sin(angle * Mathf.Deg2Rad) * 0.2f
        );

        pb.Shoot(this.gameObject, pb.transform.position, new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * 360, 0, Mathf.Sin(angle * Mathf.Deg2Rad) * 360));

    }

    private IEnumerator Counterattacked()
    {
        yield return new WaitForSeconds(3f);
        counterattacked = false;
    }


    private void ShowGroung()
    {
        float radius = 2f * Mathf.Max(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);
        // Get random enemy
        Collider2D[] camadas = Physics2D.OverlapCircleAll(this.transform.position, radius, 1 << LayerMask.NameToLayer("BlackLayer"));

        foreach (Collider2D cam in camadas)
        {
            if (cam != null)
            {
                if (cam.gameObject.name == "CamadaEscura")
                {
                    Destroy(cam.gameObject);
                }
            }
        }
    }

    private void SetEnemyDead()
    {
        bool IsGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
                                                new Vector2(transform.position.x + 0.5f, transform.position.y - 0.5f), groundedLayer);
        Debug.Log(IsGrounded);
        if (IsGrounded == false && dead == false)
        {
            dead = true;
            Debug.Log("Foi");
            PhotonManager.instance.NPCInstances("BossFatal", this.transform.position);
        }
    }
}
