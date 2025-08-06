using System.Collections;
using UnityEngine;

public class EnemyRanged : Enemy
{
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private GameObject projectilePrefab;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shot());
    }
    
    private IEnumerator Shot()
    {
        while (true)
        {
            if (playerTransform != null)
            {
                GameObject go = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
                Projectile projectile = go.GetComponent<Projectile>();
                projectile.Initialize(attackDamage, playerTransform.gameObject);
                Rigidbody2D projectileRb = go.GetComponent<Rigidbody2D>();

                Vector3 direction = (playerTransform.position - projectileSpawnPoint.position);
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
}
