using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class UndergroundWorldChanger : MonoBehaviour
{
    private const float undergroundAngle = 180;
    private const float overgroundAngle = 0;

    [SerializeField]
    private float undergroundValue;
    public float UnderGroundValue => undergroundValue;

    public bool IsInTransition { get; private set; }

    [SerializeField]
    private float animationSpeed = 3;

    [SerializeField]
    private Transform movedTransform;
    [SerializeField]
    private Transform rotatedTransform;

    [SerializeField]
    private float headHeight = 1.5f;

    [Button]
    public void GoOverground() => GoToValue(1);
    [Button]
    public void GoUnderground() => GoToValue(-1);

    private void Awake()
    {
        undergroundValue = movedTransform.localPosition.y / headHeight;
        RefreshOrientation();
    }

    private void GoToValue(float value) 
    {
        StopAllCoroutines();
        StartCoroutine(TransitionWorldCo(value));
    }

    private IEnumerator TransitionWorldCo(float targetValue)
    {
        IsInTransition = true;
        float direction = Mathf.Sign(targetValue - undergroundValue);
        while (direction * undergroundValue < direction * targetValue)
        {
            undergroundValue += direction * animationSpeed * Time.deltaTime;
            RefreshOrientation();
            yield return null;
        }

        undergroundValue = targetValue;
        RefreshOrientation();
        IsInTransition = false;
    }

    private void RefreshOrientation()
    {
        var headLocalPosition = movedTransform.localPosition;
        headLocalPosition.y = Mathf.Abs(undergroundValue) * headHeight;
        movedTransform.localPosition = headLocalPosition;

        var currentAngle = Mathf.Lerp(0f, 180f, Mathf.InverseLerp(1, -1, undergroundValue));
        var localRotation = rotatedTransform.localEulerAngles;
        localRotation.z = currentAngle;
        rotatedTransform.localEulerAngles = localRotation;
    }

    private void OnValidate()
    {
    }
}
