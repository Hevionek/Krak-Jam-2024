using UnityEngine;

public class UndergroundBoxPositioner : MonoBehaviour
{
    [SerializeField]
    private Transform followedTransform;

    [SerializeField]
    private float yOffset;

    private void LateUpdate()
    {
        var position = followedTransform.position;
        position.y += yOffset;
        position.y = Mathf.Min(0, position.y);
        transform.position = position;
    }
}
