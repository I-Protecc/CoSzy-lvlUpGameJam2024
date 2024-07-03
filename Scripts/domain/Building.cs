namespace GameJamPlaceHolderName.Scripts.domain;

public class Building
{
    public float Health { get; private set; }
    private HealthPool _healthPool;

    public bool IsDestroyed => Health <= 0; 

    public Building(float health)
    {
        this.Health = health; 
        _healthPool = new HealthPool(Health, 0, health);
    }
    
    public void Damage(float damage)
    {
        Health = _healthPool.DecreaseHealth(damage);
    }
    
    public void Kill()
    {
        Damage(Health);
    }
}