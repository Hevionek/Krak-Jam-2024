using UnityEngine;

public class ShootSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField] 
    private AudioClip shotAudioClip;
    [SerializeField]
    private ShootingController shootingController;

    private void OnEnable()
    {
        shootingController.OnBulletShot += ShootingController_OnBulletShot;        
    }

    private void ShootingController_OnBulletShot()
    {
        audioSource.PlayOneShot(shotAudioClip);
    }

    private void OnDisable()
    {
        shootingController.OnBulletShot -= ShootingController_OnBulletShot;        
    }
}
