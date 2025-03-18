using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); // Destroy projectile if the target is gone
            return;
        }

        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Destroy(gameObject);
            // You could also trigger damage logic here
        }
    }
}
