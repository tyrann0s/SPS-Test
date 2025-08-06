using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected HPBar hpBar;
    
    [SerializeField] protected float attackDelay = 2f;
    [SerializeField] protected float attackDamage = 5f;
    
    protected virtual void Start()
    {
        hpBar.Initialize(health);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        hpBar.UpdateHealth(damage);
        if (health <= 0) Died();
    }

    protected virtual void Died()
    {
        Destroy(gameObject);
    }
}
