using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public event System.Action OnBulletShot;

    [SerializeField]
    private GunAnimation gunAnimation;
    [SerializeField]
    private float maxDistance = 100f;
    [SerializeField]
    private Transform bulletOrigin;

    [SerializeField]
    private BulletsSpawner bulletsSpawner;
    [SerializeField]
    private float shootDelay = 0.5f;
    private float shootTimer = 0f;

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && shootTimer < 0)
        {
            var camera = Camera.main;
            var screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f);
            var ray = camera.ScreenPointToRay(screenCenter);

            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out var hitInfo, maxDistance))
            {
                if (hitInfo.collider.TryGetComponent<DamagableObject>(out var damagable))
                {
                    damagable.Damage();
                }
            }

            gunAnimation.PlayShootAnimation();
            shootTimer = shootDelay;
            OnBulletShot?.Invoke();
        }
    }
}
