using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private FaceController faceController;
    [SerializeField]
    private DamagableObject damagableObject;

    private void OnEnable()
    {
        damagableObject.OnDamaged += DamagableObject_OnDamaged;
        damagableObject.OnDied += DamagableObject_OnDied;
    }

    private void DamagableObject_OnDied(DamagableObject obj)
    {
        Destroy(gameObject);
    }

    private void DamagableObject_OnDamaged(DamagableObject damagable)
    {
        if (faceController) 
            faceController.SetFace(Face.Damage);
        Invoke(nameof(ReturnToNeutralFace), 1f);
    }

    private void ReturnToNeutralFace()
    {
        if (faceController)
            faceController.SetFace(Face.Neutral);
    }

    private void OnDisable()
    {
        damagableObject.OnDamaged -= DamagableObject_OnDamaged;
        damagableObject.OnDied -= DamagableObject_OnDied;
    }
}
