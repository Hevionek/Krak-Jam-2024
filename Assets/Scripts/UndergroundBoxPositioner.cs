using UnityEngine;

public class UndergroundBoxPositioner : MonoBehaviour
{
    [SerializeField]
    private Transform followedTransform;

    private void Update()
    {
        var position = followedTransform.position;
        position.y = Mathf.Min(0, position.y);
        transform.position = position;
    }
}
