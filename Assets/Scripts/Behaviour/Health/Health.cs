using UnityEngine;
using UnityEngine.Networking;
public class Health
{
    public enum HealthType
    {
        Life
    }

    public HealthType healthType;
    public ObservableAttribute<float> maxHp;
    public ObservableAttribute<float> hp;

    public Health()
    {
        healthType = HealthType.Life;
        maxHp = new ObservableAttribute<float>(0);
        hp = new ObservableAttribute<float>(0);
    }

    public void Damage(float damage)
    {
        float finalHealth = hp.Get() - damage;
        hp.Set(Mathf.Clamp(finalHealth, 0, maxHp.Get()));
    }

    public void Heal(float amount)
    {
        float finalHealth = hp.Get() + amount;
        hp.Set(Mathf.Clamp(finalHealth, 0, maxHp.Get()));
    }
}