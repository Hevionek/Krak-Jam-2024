using UnityEngine;

public class CopyForward : MonoBehaviour
{
    [SerializeField]
    private Transform copyFrom;

    private void Update()
    {
        transform.forward = copyFrom.forward;
    }
}
