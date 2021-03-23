using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Item : MonoBehaviour
{

    private PlayerController playerController;
    public GameObject ItemUI;
    public int pointsValue;
    private bool teste = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemUI != null)
        {
            if(teste == false)
            {
                teste = true;
                setValues(pointsValue);
            }
        }
    }

    public void setValues(int points)
    {
        pointsValue = points;
        ItemUi i = ItemUI.GetComponent<ItemUi>();
        if(i != null)
            i.SetItemUi(points);
    }

    private void MovePlayerToItem()
    {
        //PlayerController play = GameObject.FindObjectOfType<PlayerController>();
        playerController.MoveOnPlayer(transform.position, this.gameObject);

    }

    void OnMouseDown()
    {
        Debug.Log("_______________________________Clicou No item");

        PlayerController player = FindObjectOfType<PlayerController>();

        if (player != null)
        {

            HealthBehaviour healt = player.GetComponent<HealthBehaviour>();
            ItemUI = GetComponent<FollowWorld>().itemUi.gameObject;

            if (healt.health.hp.Get() + pointsValue > healt.health.maxHp.Get())
            {

                if (healt.health.hp.Get() + pointsValue < 100)
                    healt.health.maxHp.Set(healt.health.hp.Get() + pointsValue);
                else
                {
                    healt.health.maxHp.Set(100);
                }

            }


            healt.health.Heal((float)pointsValue);

            Destroy(this.gameObject);
            Destroy(ItemUI);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        PlayerController player = col.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            HealthBehaviour healt = player.GetComponent<HealthBehaviour>();
            ItemUI = GetComponent<FollowWorld>().itemUi.gameObject;

            if (healt.health.hp.Get() + pointsValue > healt.health.maxHp.Get())
            {

                if (healt.health.hp.Get() + pointsValue < 100)
                    healt.health.maxHp.Set(healt.health.hp.Get() + pointsValue);
                else
                {
                    healt.health.maxHp.Set(100);
                }

            }


            healt.health.Heal((float)pointsValue);

            Destroy(this.gameObject);
            Destroy(ItemUI);
        }
    }


}
