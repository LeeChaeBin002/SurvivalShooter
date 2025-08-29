using System;
using UnityEngine;


public class LivingEntity : MonoBehaviour, IDamagable
{
    public float MaxHealth = 100f;

    public float Health { get; protected set; }
    public bool IsDead { get; private set; }

    public event Action OnDeath;

    protected virtual void OnEnable()
    {
        IsDead = false;
        Health = MaxHealth;
    }
    public virtual void OnDamage(float damage, Vector3 hitpoint, Vector3 hitNormal)
    {
        Health -= damage;

        if (Health <= 0 && !IsDead)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }

        IsDead = true;
        OnDeath?.Invoke();
    }
}
