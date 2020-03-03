public class Damage
{
    public enum DamageElement
    {
        Neutral,
        Fire,
        Water,
        Earth,
        Wind,
        Eletric
    }

    public enum DamageType
    {
        Bullet,
        Slash,
        Impact
    }

    public float damage;
    public DamageElement damageElement;
    public DamageType damageType;

    public Damage() { }

    public Damage(DamageElement damageElement, DamageType damageType, float damage)
    {
        this.damageElement = damageElement;
        this.damageType = damageType;
        this.damage = damage;
    }
}
