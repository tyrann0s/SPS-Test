using System;
using System.Collections;
using Unity.Android.Types;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;

    private void Start()
    {
        StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            Transform target = GetClosestTarget();
            if (target != null)
            {
                GameObject go = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                Projectile projectile = go.GetComponent<Projectile>();
                projectile.Initialize(10f, target.gameObject);
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
            yield return new WaitForSeconds(2f);
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
}
