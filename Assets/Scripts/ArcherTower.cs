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
        collider.radius = .5f;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        // Only search for enemies every attack cooldown
        if (attackTimer >= attackCooldown)
        {
            Transform target = FindClosestEnemy();
            if (target != null)
            {
                Attack(target);
                attackTimer = 0f;
            }
        }
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float closestDistance = attackRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    void Attack(Transform target)
    {
        if (target == null) return;

        Vector3 direction = (target.position - firePoint.position).normalized;

        // Calculate the rotation angle (convert to degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation (subtract 90 degrees if the arrow starts facing up)
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);

        // Instantiate projectile with correct rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, rotation);
        projectile.GetComponent<Projectile>().damage = 1;
        projectile.GetComponent<Projectile>().SetTarget(target);
    }

    // Draw attack range in Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void OnMouseOver()
    {
        SpriteRenderer childSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        childSprite.enabled = true;
    }

    void OnMouseExit()
    {
        SpriteRenderer childSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        childSprite.enabled = false;
    }
}
