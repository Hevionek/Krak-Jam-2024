using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public event System.Action<Bullet> OnDestroyed;

    [SerializeField]
    private float maxDistance = 1000;
    [SerializeField]
    private float speed = 20;

    [SerializeField, ReadOnly]
    private float distance;

    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody
    {
        get
        {
            if ( _rigidbody == null )
                _rigidbody = GetComponent<Rigidbody>(); 
            return _rigidbody;
        }
    }

    public void Shoot()
    {
        gameObject.SetActive(true);
        Rigidbody.velocity = transform.forward * speed;
        distance = 0;
    }

    private void Update()
    {
        distance += speed * Time.deltaTime;
        if (distance > maxDistance)
            OnDestroyed?.Invoke(this);
    }

    private void OnDisable()
    {
        Rigidbody.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<DamagableObject>(out var damagable))
            damagable.Damage();

        OnDestroyed?.Invoke(this);
    }
}
