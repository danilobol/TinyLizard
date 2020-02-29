using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public GameObject target;
    public Collider2D rangeCollider;

    public delegate bool DetectorCriteria(GameObject target);
    public delegate void OnEnemyFoundEvent(GameObject target);
    public delegate void OnEnemyOutOfRangeEvent(GameObject target);

    public DetectorCriteria Criteria;
    public event OnEnemyFoundEvent OnEnemyFound;
    public event OnEnemyOutOfRangeEvent OnEnemyOutOfRange;

    private void Start()
    {
        rangeCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Criteria != null)
        {
            if (Criteria(other.gameObject))
            {
                target = other.gameObject;

                if (OnEnemyFound != null)
                    OnEnemyFound(target);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (target == other.gameObject)
        {
            OnEnemyOutOfRange(target);
            target = null;
        }
    }
}
