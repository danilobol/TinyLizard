using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "Mob/Mob")]
public class Mob : ScriptableObject
{
    [Header("Mob")]
    public string mobName;
    public CreatureType creatureType;
    public int scorePoints;
    [Header("Body")]
    public float maxHealth;
    public float size;
    [Header("Movement")]
    public float idleWalkDistance;
    public float maxHomeDistance;
    public float speed;
    public float moveDelay;
    [Header("Battle")]
    public float damage;
    public float attackRange;
    public float attackSpeed;
    public float attackDurationTime;
    [Header("Resistence")]
    public float neutralResistence;
    public float fireResistence;
    public float waterResistence;
    public float earthResistence;
    public float windResistence;
    public float eletricalResistence;
}
