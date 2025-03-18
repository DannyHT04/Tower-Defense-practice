using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public event Action OnDeath;

    void Death()
    {
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
