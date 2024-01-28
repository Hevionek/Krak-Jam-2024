using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class UndergroundWorldChanger : MonoBehaviour
{
    public event System.Action<float> OnTransitionStarted;
    public event System.Action<float> OnTransitionEnded;

    private const float undergroundAngle = 180;
    private const float overgroundAngle = 0;

    [SerializeField]
    private float undergroundValue;
    public float UndergroundValue => undergroundValue;

    public bool IsInTransition { get; private set; }

    [SerializeField]
    private float animationSpeed = 3;

    [SerializeField]
    private Transform head;

    [SerializeField]
    private float headHeight = 1.5f;

    [Button]
    public void GoOverground() => GoToValue(1);
    [Button]
    public void GoUnderground() => GoToValue(-1);

    private void Awake()
    {
        //undergroundValue = head.localPosition.y / headHeight;
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
        OnTransitionStarted?.Invoke(targetValue);
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
        OnTransitionEnded?.Invoke(targetValue);
    }

    private void RefreshOrientation()
    {
        var headLocalPosition = head.localPosition;
        headLocalPosition.y = undergroundValue * headHeight;
        head.localPosition = headLocalPosition;

        var currentAngle = Mathf.Lerp(0f, 180f, Mathf.InverseLerp(1, -1, undergroundValue));
        head.localRotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
    }
}
