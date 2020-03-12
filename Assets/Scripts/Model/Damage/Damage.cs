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

    public Damage() { }

    public Damage( float damage)
    {
        this.damage = damage;
    }
}
