using System;

public class HealthPool
{
    public float Health;

    public float maxHealth;
    private float _minHealth;

    public HealthPool(float currentHealth, float minHealth, float maxHealth)
    {
        Health = currentHealth;
        _minHealth = minHealth;
        maxHealth = maxHealth;
    }
    
    private float HealthClamp(float healthChange)
    {
        return Health = Math.Clamp(Health + healthChange, _minHealth, maxHealth);
    }

    public float DecreaseHealth(float damage)
    {
        return HealthClamp(-damage);
    }

    public float IncreaseHealth(float heal)
    {
        return HealthClamp(heal);
    }
}