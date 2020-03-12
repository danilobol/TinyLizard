using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 playerInput;
    Rigidbody2D rb;

    public float moveSpeed;

    public LayerMask groundedLayer;

    private bool dead = false;

    public CharacterStatus characterStatus;


    // Start is called before the first frame update
    void Start()
    {
        characterStatus = new CharacterStatus();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetEnemyDead();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShowGroung();
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = playerInput.normalized * moveSpeed;
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
                                                new Vector2(transform.position.x + 0.5f, transform.position.y -0.5f), groundedLayer);
        if (!IsGrounded && dead == false)
        {
            dead = true;
            Debug.Log("Foi");
            PhotonManager.instance.NPCInstances("BossFatal", this.transform.position);
        }
    }
}