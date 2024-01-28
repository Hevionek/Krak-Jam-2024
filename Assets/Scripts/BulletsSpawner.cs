using UnityEngine;
using UnityEngine.Pool;

public class BulletsSpawner : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrototype;

    private ObjectPool<Bullet> bulletsPool;

    private void Awake()
    {
        bulletsPool = new ObjectPool<Bullet>(CreateBullet);
    }

    private Bullet CreateBullet()
    {
        var bullet = Instantiate(bulletPrototype, transform);
        bullet.OnDestroyed += ReleaseBullet;
        return bullet;
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletsPool.Release(bullet);
    }

    public Bullet SpawnBullet(Vector3 position, Vector3 direction)
    {
        var bullet = bulletsPool.Get();
        bullet.transform.position = position;
        bullet.transform.forward = direction;
        return bullet;
    }
}
