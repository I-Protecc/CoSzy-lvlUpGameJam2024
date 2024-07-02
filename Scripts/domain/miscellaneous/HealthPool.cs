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

    public float DecreaseHealth(float damage)
    {
        return HealthClamp(-damage);
    }

    public float IncreaseHealth(float heal)
    {
        return HealthClamp(heal);
    }
}