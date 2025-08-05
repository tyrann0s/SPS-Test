using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private GameObject target;

    public void Initialize(float damage, GameObject target)
    {
        this.damage = damage;
        this.target = target;
        StartCoroutine(AutoDestroy());
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            Character character = other.gameObject.GetComponent<Character>();
            character.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
