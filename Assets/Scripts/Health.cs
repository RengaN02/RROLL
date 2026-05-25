using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : RenBehaviour
{
    public int currentHealth;
    public int maxHealth = 100;

    public GameObject hitEffectPrefab; // Vurulunca çıkan efekt

    public UnityEvent OnDeath;
    private int runtimeListenerCount = 0;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, transform.position + Vector3.up, Quaternion.identity);
            Destroy(effect, 1f);
        }

        if (currentHealth <= 0)
        {
            if(OnDeath.GetPersistentEventCount() + runtimeListenerCount > 0)
            {
                OnDeath?.Invoke();
            } else {
                Destroy(gameObject);
            }
        }
    }

    public void RegisterDeathListener(UnityAction call)
    {
        OnDeath.AddListener(call);
        runtimeListenerCount++;
    }
    
    public void UnregisterDeathListener(UnityAction call)
    {
        OnDeath.RemoveListener(call);
        runtimeListenerCount--;
    }

}