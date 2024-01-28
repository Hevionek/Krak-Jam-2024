using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Random.rotation;
    }
}
