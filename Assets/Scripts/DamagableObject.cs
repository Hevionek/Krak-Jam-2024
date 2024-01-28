using NaughtyAttributes;
using UnityEngine;

public class DamagableObject : MonoBehaviour
{
    public event System.Action<DamagableObject> OnDamaged;
    public event System.Action<DamagableObject> OnDied;

    [SerializeField]
    private int maxHealth;
    [SerializeField, ReadOnly]
    private int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void Damage()
    {
        health -= 1;
        OnDamaged?.Invoke(this);
        if (health <= 0)
            OnDied?.Invoke(this);
    }
}
