using System;
using System.Collections;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;

    public Action<float> OnHPChanged;
    public Action<float> OnDamageChanged;
    public Action<float> OnAttackSpeedChanged;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shot());
        
        OnHPChanged?.Invoke(health);
        OnDamageChanged?.Invoke(attackDamage);
        OnAttackSpeedChanged?.Invoke(attackDelay);
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            Transform target = GetClosestTarget();
            if (target)
            {
                GameObject go = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                Projectile projectile = go.GetComponent<Projectile>();
                projectile.Initialize(attackDamage, target.gameObject);
                Rigidbody2D projectileRb = go.GetComponent<Rigidbody2D>();

                Vector3 direction = (target.position - projectileSpawnPoint.position);
                float distance = direction.magnitude;
                float angle = 45f * Mathf.Deg2Rad;

                float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2f * angle));
                Vector3 velocityVector = new Vector3(
                    direction.x,
                    distance * Mathf.Tan(angle),
                    direction.z).normalized * velocity;

                projectileRb.linearVelocity = velocityVector;
            }
            yield return new WaitForSeconds(attackDelay);
        }
    }

    private Transform GetClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0)
            return null;

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    public void IncreaseDamage(float amount)
    {
        attackDamage += amount;
        OnDamageChanged?.Invoke(attackDamage);
    }

    public void IncreaseAttackSpeed(float amount)
    {
        attackDelay -= amount;
        OnAttackSpeedChanged?.Invoke(attackDelay);
    }
    
    public void IncreaseHealth(float amount)
    {
        health += amount;
        hpBar.IncreaseHealth(amount);
        OnHPChanged?.Invoke(health);
    }

    protected override void Died()
    {
        UIManager.Instance.ShowEndgamePanel("You lose!");
        base.Died();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnHPChanged?.Invoke(health);
    }
}
