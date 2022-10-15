using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles HealthLogic for any type of object
/// Runs events when health runs out
/// </summary>
public class HealthLogic : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int health;
    public int Health { get { return health; } private set { health = value; } }
    public int MaxHealth { get { return maxHealth; } private set { maxHealth = value; } }

    [HideInInspector]
    public UnityEvent<HealthLogic, int> E_Damage = new UnityEvent<HealthLogic, int>(), E_Heal = new UnityEvent<HealthLogic, int>();
    public UnityEvent<HealthLogic> E_Die = new UnityEvent<HealthLogic>();


    public void SetHealthMulti(float multi)
    {
        maxHealth = (int)(maxHealth * multi);
        health = maxHealth;
    }

    private void Awake()
    {
        health = maxHealth;
    }



    public void Die()
    {
        E_Die.Invoke(this);
    }

    public void FullHeal()
    {
        Heal(maxHealth);
    }

    public void Heal(int healAmt)
    {
        health = Mathf.Min(health + healAmt, maxHealth);
        E_Heal.Invoke(this, healAmt);
    }

    public void Damage(int damageAmt)
    {

        health = health - damageAmt;

        E_Damage.Invoke(this, damageAmt);

        if (health <= 0)
        {
            Die();
        }

    }
}
