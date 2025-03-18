using System.Collections.Generic;
using UnityEngine;

public class ArcherTower : MonoBehaviour
{
    public float attackRange = 3f;
    public float attackCooldown = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float attackTimer = 0f;
    void Start()
    {
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = attackRange;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (enemiesInRange.Count > 0 && attackTimer >= attackCooldown)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    void Attack()
    {
        if (enemiesInRange.Count > 0)
        {
            GameObject target = enemiesInRange[0];


            // Calculate direction to target
            Vector3 direction = (target.transform.position - firePoint.position).normalized;

            // Calculate the rotation angle (convert to degrees)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Apply rotation (subtract 90 degrees if the arrow starts facing up)
            Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);

            // Instantiate projectile with correct rotation
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
            projectile.GetComponent<Projectile>().SetTarget(target.transform);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}
