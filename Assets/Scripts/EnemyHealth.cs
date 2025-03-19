using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public event Action OnDeath;
    public int hitPoints = 1;
    public int moneyAmount = 5;
    private GameObject gameManager;

    void Start()
    {
     gameManager = GameObject.Find("GameManager");   
    }

    void Death()
    {

        OnDeath?.Invoke();
        gameManager.GetComponent<Shopping>().UpdateMoney(moneyAmount);
        Destroy(gameObject);

    }

    public void TakeDamage(int damageTaken)
    {
        hitPoints -= damageTaken;
        if (hitPoints <= 0)
        {
            Death();

        }
    }
}
