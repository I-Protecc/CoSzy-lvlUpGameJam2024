using System;

public class HealthPool
{
    public float Health;

    private float _maxHealth;
    private float _minHealth;

    public HealthPool(float currentHealth, float minHealth, float maxHealth)
    {
        Health = currentHealth;
        _minHealth = minHealth;
        _maxHealth = maxHealth;
    }
    
    private float HealthClamp(float healthChange)
    {
        return Health = Math.Clamp(Health + healthChange, _minHealth, _maxHealth);
    }

    public float Damage(int damage)
    {
        return HealthClamp(-damage);
    }

    public float Heal(int heal)
    {
        return HealthClamp(heal);
    }
}