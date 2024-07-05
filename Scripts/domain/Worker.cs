namespace GameJamPlaceHolderName.Scripts.domain;

public class Worker
{
    public float Health { get; private set; }
    private HealthPool _healthPool;

    public bool IsDead => Health <= 0; 

    public Worker(float health)
    {
        this.Health = health; 
        _healthPool = new HealthPool(Health, 0, health);
    }
    
    public void Damage(float damage)
    {
        Health = _healthPool.DecreaseHealth(damage);
    }

    public void FullHeal()
    {
        Health = _healthPool.IncreaseHealth(_healthPool.maxHealth - Health);
    }
    
    public void Kill()
    {
        Damage(Health);
    }
}