using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public List<GameObject> item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItemMob(float points)
    {
        GameObject itemDrop = Instantiate(item[Random.Range(0, item.Count)], new Vector3(this.transform.position.x, this.transform.position.y, 10), Quaternion.identity);
        FollowWorld followWorld = itemDrop.GetComponent<FollowWorld>();
        Item itemm = itemDrop.GetComponent<Item>();
        itemm.setValues((int)points);
    }
}
