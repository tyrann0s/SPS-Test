using System.Collections;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField] private float stoppingDistance = 1f;
    [SerializeField] private float moveSpeed = 2f;
    
    private Player player;
    
    protected override void Start()
    {
        base.Start();
        player = playerTransform.gameObject.GetComponent<Player>();
        StartCoroutine(MoveToPlayerCoroutine());
    }

    private IEnumerator MoveToPlayerCoroutine()
    {
        while (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            
            if (distance > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                    playerTransform.position, 
                    moveSpeed * Time.deltaTime);
            }
            else
            {
                StartCoroutine(Attack());
                yield break;
            }
            
            yield return null; // Ждём один кадр
        }
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            player.TakeDamage(attackDamage);
            yield return new WaitForSeconds(attackDelay);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}