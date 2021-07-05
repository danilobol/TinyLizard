using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine.Utility;



[RequireComponent(typeof(HealthBehaviour))]
[RequireComponent(typeof(DamageableBehaviour))]



public class PlayerController : CreatureBehaviour
{
    [Header("Dependencies")]

    public int restartLevelDelay = 1;
    //    public Character character;
    public int healt;
    public int attack;
    public HealthBehaviour healthBehaviour;
    public DamageableBehaviour damageableBehaviour;
    public GameObject visual;
    public GameObject projectilePrefab;
    public ContactFilter2D contactFilter2D;
    public LayerMask enemyLayer;

    //Animação
    private Animator anim;

    //move mouse
    private Vector3 positionDestination;
    private bool isMove = false;


    public Collider2D circleColider;


    public CharacterStatus characterStatus = new CharacterStatus();

    public LayerMask groundedLayer;

    public Vector2 playerInput;
    public LayerMask layer;
    Rigidbody2D rb;
    float horizontal = 0;     //Used to store the horizontal move direction.
    float vertical = 0;       //Used to store the vertical move direction.

    private int lifeTime = 500; //tempo de recuperação de vida

    private bool initialized = false;
    private float moveSpeed;
    private bool move = true;
    private bool turnAttack = true;
    private bool activeTime = false; //gatilho contagem regressiva para recuperação de dano 

    public GameObject target;
    private bool canAttack = true;
    private bool canMoving = true;
    public bool automaticAttack = false;
    

    //Indicador de ataque

        public GameObject indicateAttack;




    private void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Init();
        GameUIBehaviour gameUI = FindObjectOfType<GameUIBehaviour>();
        gameUI.SetOwnerPlayer(characterStatus, attack);
    }

    public void Init()
    {
        if (initialized)
            return;

        characterStatus.health = healthBehaviour.health;

        characterStatus.health.maxHp.Set(healt);
        characterStatus.health.hp.Set(healt);

        moveSpeed = 2;

        DamageableBehaviour damageableBehaviour = GetComponent<DamageableBehaviour>();

      
        initialized = true;
    }

   private IEnumerator TimeMaxMove()
   {
        yield return new WaitForSeconds(3f);
        isMove = false;

   }

    public void MoveOnPlayer(Vector3 pos)
    {
        if(turnAttack)
            this.transform.position = pos;
        
    }

    public void MoveOnPlayer(Vector3 pos, GameObject target)
    {
        this.transform.position = pos;
        this.target = target;
        if (Vector3.Distance(this.transform.position, target.transform.position) < 0.8f && turnAttack)
        {
            DamageableBehaviour damage = target.GetComponent<DamageableBehaviour>();
            Attack(damage);

        }
    }
    public void Attack(DamageableBehaviour damage)
    {

        StartCoroutine(TimeEnemyAttack(1.8f, damage));
        anim.SetTrigger("Attack");
        Debug.Log("Acertou no: " + damage.gameObject.name);

    }
    

    private void Update()
    {

        if (automaticAttack==true && turnAttack ==true)
        {
            
            Collider2D proj = Physics2D.OverlapCircle(this.transform.position, 2f, enemyLayer);
            if (proj != null)
            {
                Debug.Log("Inimigo: " + proj.name);
                DamageableBehaviour damage = proj.GetComponent<DamageableBehaviour>();

                if (damage != null && damage.creatureType != CreatureType.player)
                {
                    target = damage.gameObject;
                    automaticAttack = true;
                }

            }
                       
        }

        if(target != null && turnAttack == true)
        {
            
            if (Vector3.Distance(this.transform.position, target.transform.position) < 0.8f && automaticAttack == true)
            {
                canAttack = true;
                automaticAttack = true;
                DamageableBehaviour damage = target.GetComponent<DamageableBehaviour>();
                Attack(damage);

            }
            else
            {
                automaticAttack = false;
                target = null;
                canAttack = false;
            }
        }


        if (brain != null)
            brain.Think();
        if (healthBehaviour.health.hp.Get() <= 0f)
        {
            anim.SetTrigger("Die");
            //time animator
            //time animator
            AnimatorStateInfo animationState = anim.GetCurrentAnimatorStateInfo(0);
            AnimatorClipInfo[] myAnimatorClip = anim.GetCurrentAnimatorClipInfo(0);
            float myTime = myAnimatorClip[0].clip.length * animationState.normalizedTime;
            Debug.Log("Tempo de morte: " + myTime);
            StartCoroutine(Die(myTime));

        }
    }

    private void RecoverDamage()
    {
        if (healthBehaviour.health.hp.Get() < healthBehaviour.health.maxHp.Get())
        {
            healthBehaviour.health.Heal(1);
        }

    }

    void FixedUpdate()
    {
        if (healthBehaviour.health.hp.Get() < healthBehaviour.health.maxHp.Get())
        {
            activeTime = true;
        }
        else
        {
            activeTime = false;
        }

        if (activeTime)
        {
            if (lifeTime >= 0)
            {
                lifeTime -= 1;
                if (lifeTime == 0)
                {
                    lifeTime = 500;
                    RecoverDamage();
                }
            }
        }

        ShowGroung();
        if (move && canMoving && turnAttack && automaticAttack == false)
        {
             horizontal = 0;     //Used to store the horizontal move direction.
             vertical = 0;       //Used to store the vertical move direction.

            horizontal = (int)(Input.GetAxisRaw("Horizontal"));
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            if (canMoving == true)
            {
                playerInput = new Vector2(horizontal, vertical);
                rb.velocity = playerInput.normalized * 2f;
                StartCoroutine(Move());
            }
        }
    }

    IEnumerator Move()
    {
        vertical = 0;
        horizontal = 0;
        move = false;
        yield return new WaitForSeconds(0.1f);
        move = true;
    }


    public override void SetDefaultBrain()
    {
        brain = null;
    }

   
    private IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time+4f);
        Debug.Log("Morreu!");
        Destroy(this.gameObject);
        UIEnemy eUI = FindObjectOfType<UIEnemy>();
        eUI.DieUi();
        //SceneManager.LoadScene("Menu");
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

    //OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the tag of the trigger collided with is Exit.
        if (other.tag == "Exit")
        {
            canMoving = false;
            anim.SetTrigger("Win");

            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            Invoke("Restart", restartLevelDelay);

            //Disable the player object since level is over.
            //enabled = false;
            
            
        }

    }

    //Restart reloads the scene when called.
    private void Restart()
    {
        Completed.GameManager.instance.healthPlayer = (int)characterStatus.health.maxHp.Get();
        //Load the last scene loaded, in this case Main, the only scene in the game. And we load it in "Single" mode so it replace the existing one
        //and not load all the scene object in the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private IEnumerator TimeEnemyAttack(float time, DamageableBehaviour damage)
    {
        turnAttack = false;
        yield return new WaitForSeconds(time);
        damage.Damage(this.gameObject, new Damage(attack));
        StartCoroutine(timeToReceiveAttack(time));
    }

    private IEnumerator timeToReceiveAttack(float time)
    {
        yield return new WaitForSeconds(time);
        turnAttack = true;

    }

    public void SetLevelPlayer(int healt, int attack)
    {
      this.healt = healt;
      this.attack = attack;
    }

    public void ActivateDeadlyAttack(DamageableBehaviour damage)
    {
        MoveOnPlayer(damage.transform.position);
        Attack(damage);
        automaticAttack = true;
    }

    public void DisableDeadlyAttack()
    {
        automaticAttack = false;
    }

}
