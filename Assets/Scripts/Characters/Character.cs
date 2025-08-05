using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Died();
    }

    public void Died()
    {
        Destroy(gameObject);
    }
}
