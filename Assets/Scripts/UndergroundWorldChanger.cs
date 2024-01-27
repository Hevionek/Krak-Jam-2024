using System.Collections;
using UnityEngine;

public class UndergroundWorldChanger : MonoBehaviour
{
    private const float undergroundAngle = -180;
    private const float overgroundAngle = 0;

    [SerializeField]
    private bool isUnderground;
    public bool IsUnderground
    {
        get => isUnderground;
        set
        {
            StopAllCoroutines();
            isUnderground = value;
            StartCoroutine(TransitionWorldCo());
        }
    }

    [SerializeField]
    private float rotationSpeed = 300;

    private IEnumerator TransitionWorldCo()
    {
        float targetAngle = isUnderground ? undergroundAngle : overgroundAngle;
        float startAngle = transform.localEulerAngles.z;
        float progress = 0;
        float progressSpeed = rotationSpeed / Mathf.Abs(startAngle - targetAngle);
        while (progress < 1)
        {
            progress += Time.deltaTime * progressSpeed;
            float angle = Mathf.Lerp(startAngle, targetAngle, progress);
            transform.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return null;
        }
        transform.localRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
    }

    private void OnValidate()
    {
        if (Application.isPlaying) 
            IsUnderground = IsUnderground;
    }
}
