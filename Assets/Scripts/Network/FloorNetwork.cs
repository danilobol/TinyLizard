using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorNetwork : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] scriptsToIgnore;

    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        Initialize();
    }

    void Initialize()
    {
        if (photonView.isMine)
        {

        }
        else
        {
            foreach (MobBehaviour item in scriptsToIgnore)
            {
                item.enabled = false;
            }
        }
    }

}
