using UnityEngine;
using UnityEngine.Pool;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    private GunAnimation gunAnimation;
    [SerializeField]
    private float maxDistance = 100f;
    [SerializeField]
    private Transform bulletOrigin;

    [SerializeField]
    private Bullet bulletPrototype;

    private ObjectPool<Bullet> bulletsPool;

    private void Awake()
    {
        bulletsPool = new ObjectPool<Bullet>(SpawnBullet);
    }

    private Bullet SpawnBullet()
    {
        var bullet = Instantiate(bulletPrototype);
        bullet.OnDestroyed += ReleaseBullet;
        return bullet;
    }

    private void ReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletsPool.Release(bullet);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var camera = Camera.main;
            var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f);
            var ray = camera.ScreenPointToRay(screenCenter);
            var hitPosition = Physics.Raycast(ray, out var hitInfo, maxDistance) 
                ? hitInfo.point 
                : ray.origin + ray.direction * maxDistance;

            var bulletDirection = (hitPosition - bulletOrigin.position).normalized;
            var bullet = bulletsPool.Get();
            bullet.transform.position = bulletOrigin.position;
            bullet.transform.forward = bulletDirection;
            bullet.Shoot();
            //gunAnimation.PlayShootAnimation();
        }
    }
}
