using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField]
    private float defaultAngle;
    [SerializeField]
    private float shotAngle;

    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float animationSpeed;

    public void PlayShootAnimation()
    {
        StopAllCoroutines();
        StartCoroutine(AnimationCo());
    }

    private IEnumerator AnimationCo()
    {
        transform.localRotation = Quaternion.AngleAxis(defaultAngle, Vector3.right);
        float progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime * animationSpeed;
            float angle = Mathf.Lerp(defaultAngle, shotAngle, curve.Evaluate(progress));
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.right);
            yield return null;
        }
        transform.localRotation = Quaternion.AngleAxis(defaultAngle, Vector3.right);
    }
}
