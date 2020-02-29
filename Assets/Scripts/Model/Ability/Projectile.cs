using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Projectile/Projectile")]
public class Projectile : ScriptableObject
{
    public float damage;
    
    public float speed;
    public float duration;
}
