using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Character")]
public class Character : ScriptableObject
{
    public int characterId;
    public string characterName;

    public CreatureType creatureType;

    public float baseHealth;
    public float baseSpeed;


    public float baseDamageAttack;

}
